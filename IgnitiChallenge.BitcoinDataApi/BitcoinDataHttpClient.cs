using IgnitiChallenge.BitcoinDataApi.Helpers;
using IgnitiChallenge.BitcoinDataApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi
{
    public class BitcoinDataHttpClient : HttpClient
    {
        public BitcoinDataHttpClient()
        {
            this.BaseAddress = new Uri("https://trade.cex.io/api/spot/rest-public/get_candles");
        }

        public async Task<T> PostAsync<T>(string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await PostAsync(this.BaseAddress, content);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            T dataResponse = JsonConvert.DeserializeObject<T>(jsonResponse);

            return dataResponse;
        }
    }
}
