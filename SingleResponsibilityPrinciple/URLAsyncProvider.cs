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
        ITradeDataProvider originalProvider;
        
        public URLAsyncProvider(ITradeDataProvider originalProvider)
        {
            this.originalProvider = originalProvider;
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<IEnumerable<string>> task = Task.Run(() => originalProvider.GetTradeData()); ;
            task.Wait();
            IEnumerable<string> lines = task.Result;
            return lines;
        }
    }
}
