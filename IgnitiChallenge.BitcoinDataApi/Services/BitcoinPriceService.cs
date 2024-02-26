using IgnitiChallenge.BitcoinDataApi.Helpers;
using IgnitiChallenge.BitcoinDataApi.Interfaces;
using IgnitiChallenge.BitcoinDataApi.Model;
using IgnitiChallenge.BitcoinDataApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Services
{
    public class BitcoinPriceService : IBitcoinPriceService
    {
        private const int dataLimit = 31;
        private readonly ILogger _logger;

        public BitcoinPriceService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<List<BitcoinDataModel>> GetPriceHistoryForLast31Days()
        {
            try
            {
                using (BitcoinDataHttpClient client = new BitcoinDataHttpClient())
                {
                    string jsonData = GetJsonDataString();

                    BitcoinHttpDataResponse response = await client.PostAsync<BitcoinHttpDataResponse>(jsonData);

                    if (response.Data.Count == 0)
                    {
                        _logger.Info("No data is received.");
                    }

                    _logger.Info($"Received data count is {response.Data.Count}.");

                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new List<BitcoinDataModel>();
            }
            
        }

        public async Task<List<BitcoinDataModel>> GetPriceHistoryForTimePeriod(string fromISO, string toISO)
        {
            try
            {
                using (BitcoinDataHttpClient client = new BitcoinDataHttpClient())
                {
                    string jsonData = GetJsonDataStringForTimePeriod(Helper.GetTimeStampFromDatetime(fromISO),
                                                                     Helper.GetTimeStampFromDatetime(toISO));

                    BitcoinHttpDataResponse response = await client.PostAsync<BitcoinHttpDataResponse>(jsonData);

                    if (response.Data.Count == 0)
                    {
                        _logger.Info("No data is received.");
                    }

                    _logger.Info($"Received data count is {response.Data.Count}.");

                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new List<BitcoinDataModel>();
            }
        }

        private string GetJsonDataString(string pair = "BTC-USD", 
                                         string dataType = "bestAsk", 
                                         string resolution = "1d")
        {
            BitcoinDataParams btcParams = new BitcoinDataParams()
            {
                Pair = pair,
                FromISO = Helper.GetHistoricalDataStartDateTimestamp(),
                Limit = dataLimit,
                DataType = dataType,
                Resolution = resolution
            };

            return JsonConvert.SerializeObject(btcParams);
        }

        private string GetJsonDataStringForTimePeriod(long fromISO, long toISO, 
                                                      string pair = "BTC-USD",
                                                      string dataType = "bestAsk",
                                                      string resolution = "1d")
        {
            BitcoinDataParams btcParams = new BitcoinDataParams()
            {
                Pair = pair,
                FromISO = fromISO,
                ToISO = toISO,
                DataType = dataType,
                Resolution = resolution
            };

            return JsonConvert.SerializeObject(btcParams);
        }
    }
}
