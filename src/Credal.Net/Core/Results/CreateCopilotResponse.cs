using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Results;

public class CreateCopilotResponse
{
    [JsonPropertyName("agentId")]
    public Guid AgentId { get; set; }
}
