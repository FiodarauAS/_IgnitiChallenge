using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Model
{
    public class BitcoinDataModel
    {
        public long Timestamp { private get; set; }
        public string DateTime
        {
            get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Timestamp);
                DateTime dt = dateTimeOffset.LocalDateTime;
                return dt.ToString("MM.dd.yyyy");
            }
        }
        public double Open { get; set; }
        public double Close { get; set; }
        public string Resolution { get; set; }
    }
}
