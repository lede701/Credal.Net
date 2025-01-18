using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Results;

public class CredalResult<T>
{
    [JsonPropertyName("sendChatResult")]
    public T? Results { get; set; }

    public bool IsSuccess { get => this.Results is not null; }

    public static CredalResult<T> Failure { get => new CredalResult<T>() { Results = default }; }
    public static CredalResult<T> Success(T result) { return new CredalResult<T>() { Results = result }; }
}
