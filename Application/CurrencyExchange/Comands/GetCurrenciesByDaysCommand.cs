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
    public class GetCurrenciesByDaysCommand : IRequest<Result<GetAllCurrenciesReadModel>>
    {

        public int Days { get; set; }


    }


    public class GetCurrenciesByDaysCommandHandler : IRequestHandler<GetCurrenciesByDaysCommand, Result<GetAllCurrenciesReadModel>>
    {
        private readonly ICurrencyExchange _currencyExchange;
        private ILogger<GetCurrenciesByDaysCommandHandler> _logger;
        private readonly IExceptionLogging _exceptionLogging;
        public GetCurrenciesByDaysCommandHandler(ICurrencyExchange currencyExchange, IExceptionLogging exceptionLogging
            ,ILogger<GetCurrenciesByDaysCommandHandler> logger)
        {
            _logger = logger;
            _currencyExchange = currencyExchange;
            _exceptionLogging = exceptionLogging;
        }

        public async Task<Result<GetAllCurrenciesReadModel>> Handle(GetCurrenciesByDaysCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _currencyExchange.GetCurrenciesByNDaysAgo(request.Days);
                if (!result.Status)
                    return await Result<GetAllCurrenciesReadModel>.FailAsync(result.ReturnMessage);
                return await Result<GetAllCurrenciesReadModel>.SuccessAsync(result);
            }
            catch (Exception ex)
            {
                _exceptionLogging.SendErrorToText(ex);
                _logger.LogError("GetCurrenciesByDaysCommandHandler", ex.Message);
                return await Result<GetAllCurrenciesReadModel>.FailAsync();
            }
        }
    }
}
