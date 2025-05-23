﻿// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Results.DocumentCatalog
{
    public class SyncSourceByUrlResult
    {
        [JsonPropertyName("sourceId")]
        public Guid SourceId { get; set; }
    }
}