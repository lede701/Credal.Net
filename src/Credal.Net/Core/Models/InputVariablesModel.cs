// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

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
