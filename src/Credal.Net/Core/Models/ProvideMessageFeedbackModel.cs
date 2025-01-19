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
