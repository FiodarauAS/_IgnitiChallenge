using IgnitiChallenge.BitcoinDataApi.Helpers;
using IgnitiChallenge.BitcoinDataApi.Interfaces;
using IgnitiChallenge.BitcoinDataApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IgnitiChallenge.BitcoinDataApi.Services
{
    public class BitcoinDataUpdateService : IBitcoinDataUpdateService
    {
        private const string _fileName = "last_bitcoin_update.json";
        private const double _refreshIntervalMilliseconds = 24000 * 60 * 60;
        private readonly ILogger _logger;

        public BitcoinDataUpdateService(ILogger logger)
        {
            _logger = logger;

            if (!File.Exists(_fileName))
            {
                CreateFile();
            }
        }
        public double IntervalTillNextUpdate()
        {
            DateTime lastUpdateTime = GetLastUpdateTime();
            TimeSpan elapsedTime = DateTime.UtcNow.ToLocalTime() - lastUpdateTime;

            double nextUpdate = _refreshIntervalMilliseconds - elapsedTime.TotalMilliseconds;

            _logger.Info($"Next update in {nextUpdate}.");

            return nextUpdate;
        }
        public void UpdateData()
        {
            BitcoinLastUpdateModel lastUpdate = LastUpdate();

            string updatedJson = JsonConvert.SerializeObject(lastUpdate, Formatting.Indented);
            File.WriteAllText(_fileName, updatedJson);

            _logger.Info("Bitcoin data updated by timer.");
        }

        private DateTime GetLastUpdateTime()
        {
            string json = File.ReadAllText(_fileName);
            BitcoinLastUpdateModel jsonData = JsonConvert.DeserializeObject<BitcoinLastUpdateModel>(json);

            return jsonData.LastUpdate;
        }
        private void CreateFile()
        {
            BitcoinLastUpdateModel lastUpdate = LastUpdate();

            string jsonData = JsonConvert.SerializeObject(lastUpdate, Formatting.Indented);

            File.WriteAllText(_fileName, jsonData);

            _logger.Info("File \"last_bitcoin_update\" is created.");
        }
        private BitcoinLastUpdateModel LastUpdate()
        {
            return new BitcoinLastUpdateModel()
            {
                LastUpdate = DateTime.UtcNow.ToLocalTime()
            };
        }
    }
}
