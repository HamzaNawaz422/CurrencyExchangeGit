using Application.CurrencyExchange.Models;
using Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrencyExchange
    {
        Task<GetAllCurrenciesReadModel>  GetLatestCurrencies();
        Task<GetAllCurrenciesReadModel>  GetCurrenciesByBaseCurrency(string baseCurrency);
        Task<GetAllCurrenciesReadModel>  GetCurrenciesByNDaysAgo(int days);
        Task<CurrencyConvertReadModel> GetCurrencyConversion(CurrencyConvertRequestModel request);
    }
}
