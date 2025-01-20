// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Action;

public class ActionInputModel
{
    [JsonPropertyName("textToAppend")]
    public String TextToAppend { get; set; }

    public ActionInputModel(String textToAppend)
    {
        this.TextToAppend = textToAppend;
    }
}
