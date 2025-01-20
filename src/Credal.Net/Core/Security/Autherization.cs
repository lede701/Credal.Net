// Developed by: Leland Ede
// Created: 2025-01-18
// Updated: 2025-01-20
// Source: https://github.com/lede701/Credal.Net

namespace Credal.Net.Security;

public class Autherization
{
    public string ApiKey { get; set; } = string.Empty;
    public string SecurityType { get; set; } = "Bearer";

    public bool IsKeyValid { get => !string.IsNullOrEmpty(this.ApiKey); }
    public bool IsSecurityTypeValid { get => !string.IsNullOrEmpty(this.SecurityType); }
}
