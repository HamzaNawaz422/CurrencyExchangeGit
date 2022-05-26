using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration
{
    public class CurrencyConfiguration
    {
        public string BaseURL { get; set; }
        public string APIKey { get; set; }
        public string LatestCurrencies { get; set; }
        public string BaseCurrency { get; set; }
        public string CurrencyConversion { get; set; }

    }
}
