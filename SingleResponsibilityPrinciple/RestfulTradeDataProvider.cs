using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        private readonly string url;
        private readonly ILogger logger;
        private readonly HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        private async Task<List<string>> GetTradeAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            List<string> tradesString = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Read the content as a string and deserialize it into a List<string>
                    string content = await response.Content.ReadAsStringAsync();
                    tradesString = JsonSerializer.Deserialize<List<string>>(content);
                    logger.LogInfo("Received trade strings of length = " + tradesString.Count);
                }
                else
                {
                    logger.LogWarning("Failed to retrieve trade data.");
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning("Exception occurred while fetching trades: " + ex.Message);
            }

            return tradesString;
        }

        public async Task<IEnumerable<string>> GetTradeDataAsync()
        {
            var trades = await GetTradeAsync();

            // Return trades if it's not null, otherwise return an empty list
            return trades ?? new List<string>();
        }

    }
}