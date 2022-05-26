using Application.CurrencyExchange.Comands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : BaseApiController<CurrenciesController>
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetCurrencyCommand command = new GetCurrencyCommand();
            return Ok(await _mediator.Send(command));
        }


        [HttpGet]
        [Route("{baseCurrencies}")]
        public async Task<IActionResult> GetByBaseCurrency(string baseCurrencies)
        {
            var command = new GetCurrenciesByBaseCurrencyCommand();
            command.BaseCurrency = baseCurrencies;
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("historic/{days}")]
        public async Task<IActionResult> GetCurrenciesByDays(int days)
        {
            var command = new GetCurrenciesByDaysCommand();
            command.Days = days;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("currencies")]
        public async Task<IActionResult> CurrencyConvert(CurrencyConversionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
