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
    public class CurrencyConversionCommand : IRequest<Result<CurrencyConvertReadModel>>
    {
        public CurrencyConvertRequestModel requestModel { get; set; }


    }


    public class CurrencyConversionCommandHandler : IRequestHandler<CurrencyConversionCommand, Result<CurrencyConvertReadModel>>
    {
        private readonly ICurrencyExchange _currencyExchange;
        private readonly IExceptionLogging _exceptionLogging;
        private ILogger<CurrencyConversionCommandHandler> _logger;
        public CurrencyConversionCommandHandler(ICurrencyExchange currencyExchange, IExceptionLogging exceptionLogging
            , ILogger<CurrencyConversionCommandHandler> logger)
        {
            _currencyExchange = currencyExchange;
            _exceptionLogging = exceptionLogging;
            _logger = logger;
        }

        public async Task<Result<CurrencyConvertReadModel>> Handle(CurrencyConversionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _currencyExchange.GetCurrencyConversion(request.requestModel);
                if (!result.Status)
                    return await Result<CurrencyConvertReadModel>.FailAsync(result.ReturnMessage);
                return await Result<CurrencyConvertReadModel>.SuccessAsync(result);
            }
            catch (Exception ex)
            {
                _exceptionLogging.SendErrorToText(ex);
                _logger.LogError("CurrencyConversionCommandHandler", ex.Message);
                return await Result<CurrencyConvertReadModel>.FailAsync();
            }
        }
    }
}
