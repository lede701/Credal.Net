using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credal.Net.Results
{
    public class ResponseError
    {
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; } = -1;
        public string? ResponseBody { get; set; }
        public string? RequestUri { get; set; }
        public string? RequestMethod { get; set; }
        public string? ReasonPhrase { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
    }
}
