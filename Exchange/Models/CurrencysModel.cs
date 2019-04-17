using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Models
{
    public class CurrencysModel
    {
        public string Base { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }

    public class ConvertedCurrencyModel
    {
        public double ConversionResult { get; set; }

        public ConvertedCurrencyModel(double conversionResult)
        {
            this.ConversionResult = conversionResult;
        }
    }
}
