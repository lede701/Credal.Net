using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models;

public class CollaberatorModel
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }

    public CollaberatorModel(string email, string role)
    {
        this.Email = email;
        this.Role = role;
    }
}
