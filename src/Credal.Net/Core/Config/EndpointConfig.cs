using Credal.Net.Extensions;

namespace Credal.Net.Config;

public class EndpointConfig
{
    public string Endpoint { get; set; } = "https://api.credal.ai/api";
    public string Version { get; set; } = "v0";
    public string Device { get; set; } = "copilots";
    public string Command { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = "POST";

    public string Uri { get => new Uri(Endpoint).Combine(Version, Device, Command).ToString(); }
}
