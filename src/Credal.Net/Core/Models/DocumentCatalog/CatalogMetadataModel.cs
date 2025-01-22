// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.DocumentCatalog
{
    public class CatalogMetadataModel
    {
        [JsonPropertyName("sources")]
        public ICollection<CatalogSource> Sources { get; set; }
        public string UploadAsUserEmail { get; set; }

        public CatalogMetadataModel(string uploadAsUserEmail, ICollection<CatalogSource> sources)
        {
            this.UploadAsUserEmail = uploadAsUserEmail;
            this.Sources = sources;
        }
    }
}