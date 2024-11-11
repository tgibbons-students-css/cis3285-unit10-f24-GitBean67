using System.Collections.Generic;

namespace SingleResponsibilityPrinciple.Contracts
{
    public interface ITradeDataProvider
    {
        Task<IEnumerable<string>> GetTradeDataAsync();
    }
}