// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Clients;
using Credal.Net.Config;
using Credal.Net.Models.DocumentCatalog;
using Credal.Net.Results;
using Credal.Net.Results.DocumentCatalog;
using Credal.Net.Security;

namespace Credal.Net.DocumentCatalog
{
    [Obsolete("This class is in beta and may change in future releases.")]
    public class SyncSourceByUrl : ClientBase
    {
        public const string DEVICE = "catalog";
        public const string COMMAND = "syncSourceByUrl";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public SyncSourceByUrl(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public SyncSourceByUrl(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public SyncSourceByUrl(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<SyncSourceByUrlResult>> SendAsync(SyncSourceByUrlModel document)
        {
            this.Endpoint.Device = SyncSourceByUrl.DEVICE;
            this.Endpoint.Command = SyncSourceByUrl.COMMAND;
            return await this.Send<SyncSourceByUrlResult, SyncSourceByUrlModel>(document, true);
        }
    }
}
