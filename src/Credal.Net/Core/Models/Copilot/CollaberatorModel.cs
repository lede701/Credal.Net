// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Copilot
{
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
}