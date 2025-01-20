// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Config;
using Credal.Net.Copilot;
using Credal.Net.Core.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Security;

namespace Credal.Net.Clients;

public class CredalClient
{
    private readonly Guid _agentId;
    private readonly string _userEmail;
    private Guid? _conversationId { get; set; }
    private readonly EndpointConfig _config;
    private readonly Autherization _auth;

    public CredalClient(Guid agentId, string userEmail, string apiKey)
    {
        _agentId = agentId;
        _userEmail = userEmail;
        _config = new EndpointConfig();
        _auth = new Autherization()
        {
            ApiKey = apiKey,
        };
    }

    public async Task<CredalResult<SendChatResult>> SendMessageAsync(string message)
    {
        var client = new SendMessage(_config, new ClientHeaders(), _auth);

        var msgModel = new SendMessageModel(_agentId, message, _userEmail);
        if (_conversationId is not null && _conversationId != Guid.Empty)
        {
            msgModel.ConversationId = _conversationId.ToString();
        }

        var response = await client.SendAsync(msgModel);
        if (response.IsSuccess
            && response.Result is not null)
        {
            if (_conversationId is null
                || _conversationId == Guid.Empty
                || _conversationId != response.Result.ConversationId)
            {
                // Set new conversation idfor manager to use with tracking
                _conversationId = response.Result.ConversationId;
            }

            return response;
        }

        return CredalResult<SendChatResult>.Failure;
    }

    public async Task<bool> CreateConversation()
    {
        var client = new CreateConversation(_config, _auth);

        var response = await client.SendAsync(new BaseModel(_agentId, _userEmail));
        if (response.IsSuccess
            && response.Result is not null)
        {
            _conversationId = response.Result.ConversationId;
            return true;
        }

        return false;
    }

    public async Task ProvideFeedback(bool success, string? message)
    {
        if (_conversationId is null)
        {
            throw new ApplicationException("Conversation not created yet.");
        }
        var client = new ProvideMessageFeedback(_config, _auth);
        var feedback = new ProvideMessageFeedbackModel(_agentId
            , _userEmail
            , _conversationId.Value
            , success ? MessageFeedbackModel.Positive : MessageFeedbackModel.Negative);

        var response = await client.SendAsync(feedback);
    }

    public async Task<Guid> CreateCopilot(string name, string description)
    {
        var client = new CreateCopilot(_config, _auth);
        var results = await client.SendAsync(new CreateCopilotModel(name, description, new List<CollaberatorModel>()
        {
            new CollaberatorModel(_userEmail, "editor")
        }));

        if (results.IsSuccess
            && results.Result is not null)
        {
            return results.Result.AgentId;
        }

        return Guid.Empty;
    }

    public async Task<bool> DeleteCopilot(Guid copilotId)
    {
        var client = new DeleteCopilot(_config, _auth);
        var results = await client.SendAsync(new DeleteCopilotModel(copilotId));
        return copilotId == results!.Result!.CopilotId;
    }
}
