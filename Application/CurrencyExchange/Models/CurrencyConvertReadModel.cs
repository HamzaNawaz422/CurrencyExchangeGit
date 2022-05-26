using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CurrencyExchange.Models
{
    public class CurrencyConvertReadModel: ResponseReadModel
    {
        [JsonProperty("BaseValue")]
        public long BaseValue { get; set; }

        [JsonProperty("FromCurrency")]
        public string FromCurrency { get; set; }

        [JsonProperty("ToCurrency")]
        public string ToCurrency { get; set; }

        [JsonProperty("Result")]
        public double Result { get; set; }
    }



    public class APIResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("result")]
        public double Result { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
    }


    public partial class Query
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }
    }
}


