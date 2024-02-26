using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Interfaces
{
    public interface IBitcoinDataUpdateService
    {
        public double IntervalTillNextUpdate();
        public void UpdateData();
    }
}
