using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models;

public class CreateCopilotModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("collaborators")]
    public ICollection<CollaberatorModel> Collaberators { get; set; }

    public CreateCopilotModel(string name, string description, ICollection<CollaberatorModel> collaberators)
    {
        this.Name = name;
        this.Description = description;
        this.Collaberators = collaberators;
    }
}
