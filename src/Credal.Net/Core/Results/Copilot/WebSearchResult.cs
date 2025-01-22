// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Results.Copilot
{
    public class WebSearchResult
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }
}