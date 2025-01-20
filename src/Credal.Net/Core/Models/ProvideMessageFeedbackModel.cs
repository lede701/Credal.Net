// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models;

public class ProvideMessageFeedbackModel : BaseModel
{
    [JsonPropertyName("messageId")]
    public Guid MessageId { get; set; }
    [JsonPropertyName("messageFeedback")]
    public MessageFeedbackModel MessageFeedback { get; set; }

    public ProvideMessageFeedbackModel(Guid agentId, string userEmail, Guid messageId, MessageFeedbackModel messageFeedback) : base(agentId, userEmail)
    {
        this.MessageId = messageId;
        this.MessageFeedback = messageFeedback;
    }
}
