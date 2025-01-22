// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Copilot
{
    public class SendMessageModel : AgentEmailModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("conversationId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ConversationId { get; set; }
        [JsonPropertyName("inputVariables")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public InputVariablesModel? InputVariables { get; set; }

        public SendMessageModel(Guid agentId, string message, string userEmail) : base(agentId, userEmail)
        {
            Message = message;
        }
    }
}