// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Copilot
{
    public class AddCollectionToCopilotModel
    {
        [JsonPropertyName("copilotId")]
        public Guid CopilotId { get; set; }
        [JsonPropertyName("collectionId")]
        public Guid CollectionId { get; set; }

        public AddCollectionToCopilotModel(Guid copilotId, Guid collectionId)
        {
            this.CopilotId = copilotId;
            this.CollectionId = collectionId;
        }

        public AddCollectionToCopilotModel()
        {
            this.CopilotId = Guid.Empty;
            this.CollectionId = Guid.Empty;
        }
    }
}