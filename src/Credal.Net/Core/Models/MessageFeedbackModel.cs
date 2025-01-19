using System.Text.Json.Serialization;

namespace Credal.Net.Models;

public class MessageFeedbackModel
{
    [JsonPropertyName("feedback")]
    public string Feedback { get; set; }
    [JsonPropertyName("suggestedAnswer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SuggestedAnswer { get; set; }

    public MessageFeedbackModel(string feedback) 
    {
        this.Feedback = feedback;
    }

    public static MessageFeedbackModel Positive { get => new MessageFeedbackModel("POSITIVE"); }
    public static MessageFeedbackModel Negative { get => new MessageFeedbackModel("NEGATIVE"); }
}
