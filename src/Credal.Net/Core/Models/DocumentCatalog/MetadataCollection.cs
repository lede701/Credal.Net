using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Credal.Net.Models.DocumentCatalog
{
    public class MetadataCollection
    {
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        public MetadataCollection(Dictionary<string, string> metadata)
        {
            this.Metadata = metadata;
        }
    }
}
