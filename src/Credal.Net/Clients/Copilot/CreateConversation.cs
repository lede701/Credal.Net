// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Clients;
using Credal.Net.Config;
using Credal.Net.Models;
using Credal.Net.Results;
using Credal.Net.Results.Copilot;
using Credal.Net.Security;

namespace Credal.Net.Copilot
{
    public class CreateConversation : ClientBase
    {
        public const string DEVICE = "copilots";
        public const string COMMAND = "createConversation";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }
        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public CreateConversation(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public CreateConversation(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public CreateConversation(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<ConversationResult>> SendAsync(string message, Guid messageId)
        {
            if (this.IsDefaultValid)
            {
                return await this.SendAsync(new AgentEmailModel(this.AgentId, this.UserEmail!));
            }
            return CredalResult<ConversationResult>.Failure;
        }

        public async Task<CredalResult<ConversationResult>> SendAsync(AgentEmailModel createModel)
        {
            this.Endpoint.Device = CreateConversation.DEVICE;
            this.Endpoint.Command = CreateConversation.COMMAND;
            return await this.Send<ConversationResult, AgentEmailModel>(createModel);
        }
    }
}