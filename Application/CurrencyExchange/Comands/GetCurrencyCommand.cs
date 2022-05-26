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
    public class GetCurrencyCommand : IRequest<Result<GetAllCurrenciesReadModel>>
    {
    }


    public class GetCurrencyCommandHandler : IRequestHandler<GetCurrencyCommand, Result<GetAllCurrenciesReadModel>>
    {
        private readonly ICurrencyExchange _currencyExchange;
        private ILogger<GetCurrencyCommandHandler> _logger;
        private readonly IExceptionLogging _exceptionLogging;

        public GetCurrencyCommandHandler(ICurrencyExchange currencyExchange, IExceptionLogging exceptionLogging
            , ILogger<GetCurrencyCommandHandler> logger)
        {
            _currencyExchange = currencyExchange;
            _logger = logger;
            _exceptionLogging = exceptionLogging;
        }

        public async Task<Result<GetAllCurrenciesReadModel>> Handle(GetCurrencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _currencyExchange.GetLatestCurrencies();
                if (!result.Status)
                    return await Result<GetAllCurrenciesReadModel>.FailAsync(result.ReturnMessage);
                return await Result<GetAllCurrenciesReadModel>.SuccessAsync(result);
            }
            catch (Exception ex)
            {
                _exceptionLogging.SendErrorToText(ex);
                _logger.LogError("GetCurrencyCommandHandler", ex.Message);

                return await Result<GetAllCurrenciesReadModel>.FailAsync();
            }
        }
    }
}
