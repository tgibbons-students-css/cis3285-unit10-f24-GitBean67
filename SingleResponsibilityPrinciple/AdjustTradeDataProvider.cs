using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    class AdjustTradeDataProvider : ITradeDataProvider
    {
        ITradeDataProvider originalProvider;
        public AdjustTradeDataProvider(ITradeDataProvider originalProvider) 
        {
            originalProvider = originalProvider;
        }


        public IEnumerable<string> GetTradeData()
        {
            //call original trade data
            IEnumerable<String> lines = originalProvider.GetTradeData();

            List<String> result = new List<String>();

            //change "GBP" to "UER" in all text lines
            foreach (String line in lines)
            {
                result.Add(line.Replace("GBP", "EUR")) ;
            }

            return lines;
        }
    }
}
