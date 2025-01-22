// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Clients;
using Credal.Net.Config;
using Credal.Net.Models.Copilot;
using Credal.Net.Results;
using Credal.Net.Results.Copilot;
using Credal.Net.Security;

namespace Credal.Net.Copilot
{
    public class SendMessage : ClientBase
    {
        public const string DEVICE = "copilots";
        public const string COMMAND = "sendMessage";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public SendMessage(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public SendMessage(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public SendMessage(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<SendChatResult>> SendAsync(string message)
        {
            if (this.IsDefaultValid)
            {
                return await this.SendAsync(new SendMessageModel(this.AgentId, message, this.UserEmail!));
            }
            return CredalResult<SendChatResult>.Failure;
        }

        public async Task<CredalResult<SendChatResult>> SendAsync(SendMessageModel message)
        {
            this.Endpoint.Device = SendMessage.DEVICE;
            this.Endpoint.Command = SendMessage.COMMAND;
            return await this.Send<SendChatResult, SendMessageModel>(message);
        }
    }
}