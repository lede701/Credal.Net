using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Results;

public class ExternalResourceResult
{
    [JsonPropertyName("externalResourceId")]
    public string ExternalResourceId { get; set; } = string.Empty;
    [JsonPropertyName("resourceType")]
    public string ResourceType { get; set; } = string.Empty;
}
