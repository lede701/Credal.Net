// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using Credal.Net.Core.Models.Shared;
using System.Text.Json.Serialization;

namespace Credal.Net.Models.DocumentCatalog
{
    public class CatalogSource
    {
        [JsonPropertyName("resourceIdentifier")]
        public ResourceIdentifierModel ResourceIdentifier { get; set; }
        [JsonPropertyName("metadata")]
        public MetadataCollection Metadata { get; set; }

        public CatalogSource(ResourceIdentifierModel resourceIdentifier, MetadataCollection metadata)
        {
            ResourceIdentifier = resourceIdentifier;
            Metadata = metadata;
        }
    }
}