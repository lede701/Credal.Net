using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Results;

public class DeleteCopilotResult
{
    [JsonPropertyName("copilotId")]
    public Guid CopilotId { get; set; }
}
