using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credal.Net.Core.Config;

public class ClientHeaders
{
    private Dictionary<string, string> _headers = new Dictionary<string, string>();

    public bool HasHeaders { get => this._headers.Count > 0; }

    public ICollection<string> Keys { get => _headers.Keys; }
    public ICollection<string> Values { get => _headers.Values; }

    public Dictionary<string, string> KeyValuePairs { get => this._headers; }

    public bool TryGet(string key, out string? value)
    {
        return _headers.TryGetValue(key, out value);
    }

    public bool Add(string key, string value)
    {
        if (!this._headers.ContainsKey(key))
        {
            this._headers.Add(key, value);
            return true;
        }
        return false;
    }

    public bool Update(string key, string value)
    {
        if(this._headers.ContainsKey(key))
        {
            this._headers[key] = value;
            return true;
        }
        return false;
    }

}
