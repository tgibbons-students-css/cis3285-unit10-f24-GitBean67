using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLAsyncProvider : ITradeDataProvider
    {
        private readonly string tradeUrl;
        private readonly ILogger logger;

        public URLAsyncProvider(string tradeUrl, ILogger logger)
        {
            this.tradeUrl = tradeUrl;
            this.logger = logger;
        }

        public async Task<IEnumerable<string>> GetTradeDataAsync()
        {
            List<string> tradeData = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send the request asynchronously
                    HttpResponseMessage response = await client.GetAsync(tradeUrl);
                    response.EnsureSuccessStatusCode();

                    // Read response data asynchronously
                    string content = await response.Content.ReadAsStringAsync();

                    // Process the response to create trade data entries
                    tradeData.AddRange(content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
                }
                catch (Exception ex)
                {
                    logger.LogWarning("Failed to retrieve trade data: " + ex.Message);
                }
            }

            return tradeData;
        }
    }
}
