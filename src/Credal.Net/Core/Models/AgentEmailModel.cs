// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models
{
    public class AgentEmailModel
    {
        [JsonPropertyName("agentId")]
        public Guid AgentId { get; set; }
        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }

        public AgentEmailModel(Guid agentId, string userEmail)
        {
            this.AgentId = agentId;
            this.UserEmail = userEmail;
        }
    }
}