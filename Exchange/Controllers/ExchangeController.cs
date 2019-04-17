using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Exchange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Exchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private IMemoryCache _cache;

        [HttpGet("{from}/{to}/{value}")]
        public async Task<ConvertedCurrencyModel> Get(string from, string to, double value, IMemoryCache memoryCache)
        {
            _cache = memoryCache;

            string url = "https://api.exchangeratesapi.io/latest?base=" + from.ToUpper();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CurrencysModel>(json);

                    var convertedCurrency = result.Rates.SingleOrDefault(p => p.Key == to.ToUpper()).Value * value;

                    return new ConvertedCurrencyModel(convertedCurrency);
                }                
            }
        }
    }   
}