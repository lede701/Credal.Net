using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models;

public class BaseModel
{
    [JsonPropertyName("agentId")]
    public Guid AgentId { get; set; }
    [JsonPropertyName("userEmail")]
    public string UserEmail { get; set; }

    public BaseModel(Guid agentId, string userEmail)
    {
        this.AgentId = agentId;
        this.UserEmail = userEmail;
    }
}
