// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

using System.Text.Json.Serialization;

namespace Credal.Net.Results
{
    public class CredalResult<T>
    {
        [JsonPropertyName("sendChatResult")]
        public T? Result { get; set; }

        public bool IsSuccess { get => this.Result is not null; }

        public static CredalResult<T> Failure { get => new CredalResult<T>() { Result = default }; }
        public static CredalResult<T> Success(T result) { return new CredalResult<T>() { Result = result }; }
    }
}