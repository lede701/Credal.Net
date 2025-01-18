using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credal.Net.Security;

public class Autherization
{
    public string ApiKey { get; set; } = string.Empty;
    public string SecurityType { get; set; } = "Bearer";

    public bool IsKeyValid { get => !string.IsNullOrEmpty(this.ApiKey); }
    public bool IsSecurityTypeValid { get => !string.IsNullOrEmpty(this.SecurityType); }

    public override string ToString()
    {
        if (!this.IsKeyValid)
        {
            throw new ArgumentException("ApiKey is required");
        }
        if(!this.IsSecurityTypeValid)
        {
            throw new ArgumentException("SecurityType is required");
        }

        return $"{SecurityType} {ApiKey}";
    }
}
