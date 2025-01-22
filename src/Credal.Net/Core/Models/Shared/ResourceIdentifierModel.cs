// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Models.DocumentCatalog;
using System.Text.Json.Serialization;

namespace Credal.Net.Core.Models.Shared
{
    public class ResourceIdentifierModel
    {
        [JsonPropertyName("type")]
        public string IdentifierType { get; set; }
        [JsonPropertyName("externalResourceId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExternalResourceId { get; set; }
        [JsonPropertyName("resourceType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CatalogResourceType? ResourceType { get; set; }
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        public ResourceIdentifierModel(string externalResourceId, CatalogResourceType resourceType)
        {
            this.IdentifierType = "external-resource-id";
            this.ExternalResourceId = externalResourceId;
            this.ResourceType = resourceType;
            this.Url = null;
        }

        public ResourceIdentifierModel(string url)
        {
            this.IdentifierType = "url";
            this.ExternalResourceId = null;
            this.ResourceType = null;
            this.Url = url;
        }
    }
}
