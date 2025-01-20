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
