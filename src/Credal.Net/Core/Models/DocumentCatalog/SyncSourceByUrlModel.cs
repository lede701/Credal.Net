﻿// Developed by: Leland Ede
// Created: 2025-01-22
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.DocumentCatalog
{
    public class SyncSourceByUrlModel
    {
        [JsonPropertyName("uploadAsUserEmail")]
        public string UploadAsUserEmail { get; set; }
        [JsonPropertyName("sourceUrl")]
        public string SourceUrl { get; set; }

        public SyncSourceByUrlModel(string uploadAsUserEmail, string sourceUrl)
        {
            this.UploadAsUserEmail = uploadAsUserEmail;
            this.SourceUrl = sourceUrl;
        }
    }
}