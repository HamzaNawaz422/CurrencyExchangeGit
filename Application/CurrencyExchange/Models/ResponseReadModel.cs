using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CurrencyExchange.Models
{
    public class ResponseReadModel
    {
        public string ReturnMessage { get; set; }
        public bool Status { get; set; } = true;
    }
}
