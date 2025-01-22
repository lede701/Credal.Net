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

    public class CatalogMetadata : ClientBase
    {
        public const string DEVICE = "catalog";
        public const string COMMAND = "syncSourceByUrl";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public CatalogMetadata(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public CatalogMetadata(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public CatalogMetadata(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<CatalogMetadataResult>> SendAsync(CatalogMetadataModel document)
        {
            this.Endpoint.Device = CatalogMetadata.DEVICE;
            this.Endpoint.Command = CatalogMetadata.COMMAND;
            this.Endpoint.HttpMethod = "PATCH";
            return await this.Send<CatalogMetadataResult, CatalogMetadataModel>(document, true);
        }
    }
}