using IgnitiChallenge.BitcoinDataApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Interfaces
{
    public interface IBitcoinPriceService
    {
        public Task<List<BitcoinDataModel>> GetPriceHistoryForLast31Days();
        public Task<List<BitcoinDataModel>> GetPriceHistoryForTimePeriod(string fromISO, string toISO);
    }
}
