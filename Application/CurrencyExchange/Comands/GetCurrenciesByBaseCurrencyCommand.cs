using Application.CurrencyExchange.Models;
using Application.Interfaces;
using Application.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CurrencyExchange.Comands
{
    public class GetCurrenciesByBaseCurrencyCommand : IRequest<Result<GetAllCurrenciesReadModel>>
    {

        public string BaseCurrency { get; set; }


    }


    public class GetCurrenciesByBaseCurrencyCommandHandler : IRequestHandler<GetCurrenciesByBaseCurrencyCommand, Result<GetAllCurrenciesReadModel>>
    {
        private readonly ICurrencyExchange _currencyExchange;
        private ILogger<GetCurrenciesByBaseCurrencyCommandHandler> _logger;
        private readonly IExceptionLogging _exceptionLogging;
        public GetCurrenciesByBaseCurrencyCommandHandler(ICurrencyExchange currencyExchange, IExceptionLogging exceptionLogging
            , ILogger<GetCurrenciesByBaseCurrencyCommandHandler> logger)
        {
            _currencyExchange = currencyExchange;
            _logger = logger;
            _exceptionLogging = exceptionLogging;
        }

        public async Task<Result<GetAllCurrenciesReadModel>> Handle(GetCurrenciesByBaseCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _currencyExchange.GetCurrenciesByBaseCurrency(request.BaseCurrency);
                if (!result.Status)
                    return await Result<GetAllCurrenciesReadModel>.FailAsync(result.ReturnMessage);
                return await Result<GetAllCurrenciesReadModel>.SuccessAsync(result);
            }
            catch (Exception ex)
            {
                _exceptionLogging.SendErrorToText(ex);
                _logger.LogError("GetCurrenciesByBaseCurrencyCommandHandler", ex.Message);
                return await Result<GetAllCurrenciesReadModel>.FailAsync();
            }
        }
    }
}
