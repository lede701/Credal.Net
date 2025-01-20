using Credal.Net.Clients;
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

public class CreateCopilot : ClientBase
{
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
        this.Endpoint.Command = CreateCopilot.COMMAND;
        return await this.Send<CreateCopilotResponse, CreateCopilotModel>(copilot, true);
    }
}
