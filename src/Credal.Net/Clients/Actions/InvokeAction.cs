// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net


using Credal.Net.Clients;
using Credal.Net.Config;
using Credal.Net.Core.Config;
using Credal.Net.Models.Action;
using Credal.Net.Results;
using Credal.Net.Results.Action;
using Credal.Net.Security;

namespace Credal.Net.Actions;

[Obsolete("This class is in beta and may change in future releases.")]
public class InvokeAction : ClientBase
{
    public const string DEVICE = "actions";
    public const string COMMAND = "sendMessage";
    public Guid AgentId { get; set; }
    public string? UserEmail { get; set; }

    public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

    public InvokeAction(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

    public InvokeAction(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
    public InvokeAction(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

    public async Task<CredalResult<InvokeActionResult>> SendAsync(string message)
    {
        if (this.IsDefaultValid)
        {
            return await this.SendAsync(new InvokeActionModel(this.AgentId
                , this.UserEmail!
                , new HumanConfirmationChannelModel("n/a", "n/a",TimeProvider.System.GetLocalNow().DateTime.ToString("F"))
                , new ActionInputModel(message)
                , "justification"
                , Guid.NewGuid()
            ));
        }
        return CredalResult<InvokeActionResult>.Failure;
    }

    public async Task<CredalResult<InvokeActionResult>> SendAsync(InvokeActionModel message)
    {
        this.Endpoint.Device = InvokeAction.DEVICE;
        this.Endpoint.Command = InvokeAction.COMMAND;
        return await this.Send<InvokeActionResult, InvokeActionModel>(message);
    }
}
