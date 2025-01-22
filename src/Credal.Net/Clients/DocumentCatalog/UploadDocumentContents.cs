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
    public class UploadDocumentContents : ClientBase
    {
        public const string DEVICE = "catalog";
        public const string COMMAND = "uploadDocumentContents";
        public Guid AgentId { get; set; }
        public string? UserEmail { get; set; }

        public bool IsDefaultValid { get => this.AgentId != Guid.Empty && !string.IsNullOrEmpty(this.UserEmail); }

        public UploadDocumentContents(EndpointConfig endpoint, Autherization autherization) : base(autherization, new ClientHeaders(), endpoint) { }

        public UploadDocumentContents(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization) : base(autherization, headers, endpoint) { }
        public UploadDocumentContents(EndpointConfig endpoint, ClientHeaders headers, Autherization autherization, HttpClient client) : base(autherization, headers, endpoint, client) { }

        public async Task<CredalResult<UploadDocumentContentsResult>> SendAsync(UploadDocumentContentsModel document)
        {
            this.Endpoint.Device = UploadDocumentContents.DEVICE;
            this.Endpoint.Command = UploadDocumentContents.COMMAND;
            return await this.Send<UploadDocumentContentsResult, UploadDocumentContentsModel>(document, true);
        }
    }
}