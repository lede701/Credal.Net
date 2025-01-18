using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models;

public class InputVariablesModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("ids")]
    public ICollection<string>? Ids { get; set; }

    public InputVariablesModel() { }
    public InputVariablesModel(string name, ICollection<string> ids)
    {
        this.Name = name;
        this.Ids = ids;
    }
}
