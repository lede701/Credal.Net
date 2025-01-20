// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models;

public class DeleteCopilotModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public DeleteCopilotModel(Guid id)
    {
        this.Id = id;
    }
}
