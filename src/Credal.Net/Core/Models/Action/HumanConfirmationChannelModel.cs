// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-22
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Models.Action
{
    public class HumanConfirmationChannelModel
    {
        [JsonPropertyName("type")]
        public string ChannelType { get; set; }
        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; }
        [JsonPropertyName("threadTimestamp")]
        public string ThreadTimestamp { get; set; }

        public HumanConfirmationChannelModel(string channelType, string channelId, string threadTimestamp)
        {
            this.ChannelType = channelType;
            this.ChannelId = channelId;
            this.ThreadTimestamp = threadTimestamp;
        }
    }
}