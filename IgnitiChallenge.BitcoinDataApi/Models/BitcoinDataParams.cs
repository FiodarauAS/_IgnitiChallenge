using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Models
{
    public class BitcoinDataParams
    {
        [JsonProperty("pair")]
        public string Pair { get; set; }

        [JsonProperty("fromISO")]
        public long FromISO { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("toISO")]
        public long ToISO { get; set; }

        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }
    }
}
