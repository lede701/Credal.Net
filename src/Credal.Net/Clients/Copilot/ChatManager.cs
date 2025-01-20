using Credal.Net.Config;
using Credal.Net.Core.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credal.Net.Copilot;

public class ChatManager
{
    private readonly Guid _agentId;
    private readonly string _userEmail;
    private Guid _conversationId { get; set; }
    private readonly EndpointConfig _config;
    private readonly Autherization _auth;

    public ChatManager(Guid agentId, string userEmail, string apiKey)
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
        var client = new SendMessage(this._config, new ClientHeaders(), _auth);

        var msgModel = new SendMessageModel(this._agentId, message, this._userEmail);
        if (this._conversationId != Guid.Empty)
        {
            msgModel.ConversationId = this._conversationId.ToString();
        }

        var response = await client.SendAsync(msgModel);
        if (response.IsSuccess
            && response.Results is not null)
        {
            if (this._conversationId == Guid.Empty 
                || this._conversationId != response.Results.ConversationId)
            {
                this._conversationId = response.Results.ConversationId;
            }

            return response;
        }

        return CredalResult<SendChatResult>.Failure;
    }

    public async Task<bool> CreateConversation()
    {
        var client = new CreateConversation(_config, _auth);

        var response = await client.SendAsync(new BaseModel(this._agentId, this._userEmail));
        if (response.IsSuccess
            && response.Results is not null)
        {
            this._conversationId = response.Results.ConversationId;
            return true;
        }

        return false;
    }

    public async Task ProvideFeedback(bool success, string? message)
    {
        var client = new ProvideMessageFeedback(_config, _auth);
        var feedback = new ProvideMessageFeedbackModel(this._agentId
            , this._userEmail
            , this._conversationId
            , success ? MessageFeedbackModel.Positive : MessageFeedbackModel.Negative);

        var response = await client.SendAsync(feedback);
    }

    public async Task<Guid> CreateCopilot(string name, string description)
    {
        var client = new CreateCopilot(_config, _auth);
        var results = await client.SendAsync(new CreateCopilotModel(name, description, new List<CollaberatorModel>()
        {
            new CollaberatorModel(this._userEmail, "editor")
        }));

        if (results.IsSuccess
            && results.Results is not null)
        {
            return results.Results.AgentId;
        }

        return Guid.Empty;
    }

    public async Task<bool> DeleteCopilot(Guid copilotId)
    {
        var client = new DeleteCopilot(_config, _auth);
        var results = await client.SendAsync(new DeleteCopilotModel(copilotId));
        return copilotId == results!.Results!.CopilotId;
    }
}
