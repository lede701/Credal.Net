// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Copilot
{
    public class ProvideMessageFeedbackModel : AgentEmailModel
    {
        [JsonPropertyName("messageId")]
        public Guid MessageId { get; set; }
        [JsonPropertyName("messageFeedback")]
        public MessageFeedbackModel MessageFeedback { get; set; }

        public ProvideMessageFeedbackModel(Guid agentId, string userEmail, Guid messageId, MessageFeedbackModel messageFeedback) : base(agentId, userEmail)
        {
            MessageId = messageId;
            MessageFeedback = messageFeedback;
        }
    }
}