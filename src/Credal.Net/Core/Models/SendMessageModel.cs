using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models;

public class SendMessageModel : BaseModel
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
