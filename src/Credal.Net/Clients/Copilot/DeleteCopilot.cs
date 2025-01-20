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

public class DeleteCopilot : ClientBase
{
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
        this.Endpoint.Command = SendMessage.COMMAND;
        return await this.Send<DeleteCopilotResult, DeleteCopilotModel>(copilot, true);
    }
}
