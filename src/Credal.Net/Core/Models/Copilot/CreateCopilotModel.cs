// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Copilot
{
    public class CreateCopilotModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("collaborators")]
        public ICollection<CollaberatorModel> Collaberators { get; set; }

        public CreateCopilotModel(string name, string description, ICollection<CollaberatorModel> collaberators)
        {
            this.Name = name;
            this.Description = description;
            this.Collaberators = collaberators;
        }
    }
}