using Application.Configuration;
using Application.CurrencyExchange.Models;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrencyExchange : ICurrencyExchange
    {
        private readonly CurrencyConfiguration _curConfig;
        private readonly IExceptionLogging _exceptionLogging;
        private ILogger<CurrencyExchange> _logger;
        public CurrencyExchange(IOptions<CurrencyConfiguration> curConfig, IExceptionLogging exceptionLogging
            , ILogger<CurrencyExchange> logger)
        {
            _curConfig = curConfig.Value;
            _exceptionLogging = exceptionLogging;
            _logger = logger;
        }
        public async Task<GetAllCurrenciesReadModel> GetLatestCurrencies()
        {
            var currencies = new GetAllCurrenciesReadModel();
            try
            {
                var client = new RestClient($"{_curConfig.BaseURL}{_curConfig.LatestCurrencies}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", _curConfig.APIKey);

                var response = await client.ExecuteGetAsync<GetAllCurrenciesReadModel>(request);
                return response.Data;

            }
            catch (Exception ex)
            {
                _logger.LogError("GetLatestCurrencies", ex.Message);
                _exceptionLogging.SendErrorToText(ex);
                currencies.Status = false;
                currencies.ReturnMessage = "something went wrong";
                return currencies;
            }
            
        }



        public async Task<GetAllCurrenciesReadModel> GetCurrenciesByBaseCurrency(string baseCurrency)
        {
            var currencies = new GetAllCurrenciesReadModel();
            try
            {
                var client = new RestClient($"{_curConfig.BaseURL}{_curConfig.BaseCurrency}{baseCurrency}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", _curConfig.APIKey);


                var response = await client.ExecuteGetAsync<GetAllCurrenciesReadModel>(request);
                var res = response.Data;
                return res;

            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrenciesByBaseCurrency", ex.Message);
                _exceptionLogging.SendErrorToText(ex);
                currencies.Status = false;
                currencies.ReturnMessage = "something went wrong";
                return currencies;
            }
            
        }



        public async Task<GetAllCurrenciesReadModel> GetCurrenciesByNDaysAgo(int days)
        {
            var currencies = new GetAllCurrenciesReadModel();
            try
            {

                var dateTime = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");

                var client = new RestClient($"{_curConfig.BaseURL}/{dateTime}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", _curConfig.APIKey);

                var response = await client.ExecuteGetAsync<GetAllCurrenciesReadModel>(request);
                return response.Data;


            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrenciesByNDaysAgo", ex.Message);
                _exceptionLogging.SendErrorToText(ex);
                currencies.Status = false;
                currencies.ReturnMessage = "something went wrong";
                return currencies;
            }
        }



        public async Task<CurrencyConvertReadModel> GetCurrencyConversion(CurrencyConvertRequestModel requestModel)
        {
            var currencies = new CurrencyConvertReadModel();
            try
            {
                var endpoint = $"{_curConfig.BaseURL}{string.Format(_curConfig.CurrencyConversion, requestModel.ToCurrency, requestModel.FromCurrency, requestModel.Value)}";
                var client = new RestClient(endpoint);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", _curConfig.APIKey);

                var apiResponse = await client.ExecuteGetAsync<APIResponseModel>(request);

                var response = new CurrencyConvertReadModel
                {
                    BaseValue = apiResponse.Data.Query.Amount,
                    FromCurrency = apiResponse.Data.Query.From,
                    ToCurrency = apiResponse.Data.Query.To,
                    Result = apiResponse.Data.Result
                };


                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("GetCurrencyConversion", ex.Message);
                _exceptionLogging.SendErrorToText(ex);
                currencies.Status = false;
                currencies.ReturnMessage = "something went wrong";
                return currencies;

               
            }
            
        }
    }
}
