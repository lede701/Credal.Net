// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Action;

public class InvokeActionModel
{
    [JsonPropertyName("actionId")]
    public Guid ActionId { get; set; }
    [JsonPropertyName("actionInput")]
    public ActionInputModel ActionInput { get; set; }
    [JsonPropertyName("userEmail")]
    public string UserEmail { get; set; }
    [JsonPropertyName("humanConfirmationChannel")]
    public HumanConfirmationChannelModel HumanConfirmationChannel { get; set; }
    [JsonPropertyName("justification")]
    public string Justification { get; set; }
    public Guid AuditLogId { get; set; }
    [JsonPropertyName("requireHumanConfirmation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RequireHumanConfirmation { get; set; }


    public InvokeActionModel(Guid actionId
        , string userEmail
        , HumanConfirmationChannelModel humanConfirmationChannel
        , ActionInputModel actionInput
        , string justification
        , Guid auditLogId
        )
    {
        this.ActionId = actionId;
        this.UserEmail = userEmail;
        this.HumanConfirmationChannel = humanConfirmationChannel;
        this.ActionInput = actionInput;
        this.Justification = justification;
        this.AuditLogId = auditLogId;
    }
}
