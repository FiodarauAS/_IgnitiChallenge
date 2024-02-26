using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Model
{
    public class BitcoinHttpDataResponse
    {
        public string Ok { get; set; }
        public List<BitcoinDataModel> Data { get; set; }
    }
}
