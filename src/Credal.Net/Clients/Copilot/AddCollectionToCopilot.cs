// Developed by: Leland Ede
// Created: 2025-01-22
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
    public class AddCollectionToCopilot : ClientBase
    {
        public const string COMMAND = "addCollectionToCopilot";
        public const string DEVICE = "copilots";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public AddCollectionToCopilot(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public AddCollectionToCopilot(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public AddCollectionToCopilot(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<AddCollectionToCopilotResponse>> SendAsync(string copilotId, string collectionId)
        {
            if (this.IsDefaultValid
                && Guid.TryParse(copilotId, out Guid copilotGuid)
                && Guid.TryParse(collectionId, out Guid collectionGuid))
            {
                return await this.SendAsync(new AddCollectionToCopilotModel(copilotGuid, collectionGuid));
            }
            return CredalResult<AddCollectionToCopilotResponse>.Failure;
        }

        public async Task<CredalResult<AddCollectionToCopilotResponse>> SendAsync(AddCollectionToCopilotModel copilot)
        {
            this.Endpoint.Device = AddCollectionToCopilot.DEVICE;
            this.Endpoint.Command = AddCollectionToCopilot.COMMAND;
            return await this.Send<AddCollectionToCopilotResponse, AddCollectionToCopilotModel>(copilot, true);
        }
    }
}