using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Results;

public class SendChatResult
{
    [JsonPropertyName("type")]
    public string ResultType { get; set; } = string.Empty;
    [JsonPropertyName("conversationId")]
    public Guid ConversationId { get; set; }
    [JsonPropertyName("inserted_audit_log")]
    public InsertAuditLogResult InsertAuditLog { get; set; } = new InsertAuditLogResult();
    [JsonPropertyName("messageId")]
    public Guid MessageId { get; set; }
    [JsonPropertyName("policy_triggers")]
    public ICollection<object> PolicyTriggers { get; set; } = new List<object>();
    [JsonPropertyName("referencedSources")]
    public ICollection<ReferencedSourceResult> ReferencedSources { get; set; } = new List<ReferencedSourceResult>();
    [JsonPropertyName("response")]
    public ResponseResult Response { get; set; } = new ResponseResult();
    [JsonPropertyName("sourcesInDataContext")]
    public ICollection<SourceInDataContextResult> SourceInDataContext { get; set; } = new List<SourceInDataContextResult>();
    [JsonPropertyName("warnings")]
    public ICollection<string> Warnings { get; set; } = new List<string>();
    [JsonPropertyName("webSearchResults")]
    public ICollection<WebSearchResult> WebSearchResults { get; set; } = new List<WebSearchResult>();

}
