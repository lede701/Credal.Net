// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Clients;
using Credal.Net.Config;
using Credal.Net.Models.Copilot;
using Credal.Net.Results;
using Credal.Net.Results.Copilot;
using Credal.Net.Security;

namespace Credal.Net.Copilot
{
    [Obsolete("This class is in beta and may change in future releases.")]
    public class DeleteCopilot : ClientBase
    {
        public const string DEVICE = "copilots";
        public const string COMMAND = "deleteCopilot";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public DeleteCopilot(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public DeleteCopilot(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public DeleteCopilot(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<DeleteCopilotResult>> SendAsync(string copilotId)
        {
            if (this.IsDefaultValid)
            {
                return await this.SendAsync(new DeleteCopilotModel(Guid.Parse(copilotId)));
            }
            return CredalResult<DeleteCopilotResult>.Failure;
        }

        public async Task<CredalResult<DeleteCopilotResult>> SendAsync(DeleteCopilotModel copilot)
        {
            this.Endpoint.Device = SendMessage.DEVICE;
            this.Endpoint.Command = SendMessage.COMMAND;
            this.Endpoint.HttpMethod = "delete";
            return await this.Send<DeleteCopilotResult, DeleteCopilotModel>(copilot, true);
        }
    }
}