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
    public class CreateCopilot : ClientBase
    {
        public const string DEVICE = "copilots";
        public const string COMMAND = "createCopilot";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public CreateCopilot(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public CreateCopilot(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public CreateCopilot(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<CreateCopilotResponse>> SendAsync(string name, string description, string email, string role)
        {
            if (this.IsDefaultValid)
            {
                return await this.SendAsync(new CreateCopilotModel(name, description, new List<CollaberatorModel>()
            {
                new CollaberatorModel(email, role)
            }));
            }
            return CredalResult<CreateCopilotResponse>.Failure;
        }

        public async Task<CredalResult<CreateCopilotResponse>> SendAsync(CreateCopilotModel copilot)
        {
            this.Endpoint.Device = CreateCopilot.DEVICE;
            this.Endpoint.Command = CreateCopilot.COMMAND;
            return await this.Send<CreateCopilotResponse, CreateCopilotModel>(copilot, true);
        }
    }
}