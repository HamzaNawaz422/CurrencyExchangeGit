using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CurrencyExchange.Models
{
    public class CurrencyConvertRequestModel
    {
        public decimal Value { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
    }
}
