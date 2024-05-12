using CurencyCrunchActivity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CurencyCrunchActivity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CurrencyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrencyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("listquotes")]
        public async Task<IActionResult> GetCurrencyQuotes()
        {
            var client = _httpClientFactory.CreateClient("CurrencyConverter");
            var response = await client.GetAsync("https://currency-exchange.p.rapidapi.com/listquotes");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            //Converting the JSON object to the list
            var quotes = JsonConvert.DeserializeObject < List<string>>(body);

            var result = new CurrencyQuoteList
            {
                Quotes = quotes
            };

            return Ok(result);
        }

        [HttpGet("exchange")]
        public async Task<IActionResult> GetExchangeRate(string from, string to, decimal quantity)
        {
            var client = _httpClientFactory.CreateClient("CurrencyConverter");
            var url = $"https://currency-exchange.p.rapidapi.com/exchange?from={from}&to={to}&q={quantity}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            
            //deserialsie the JSON object
            var exchangeRate = JsonConvert.DeserializeObject<decimal>(body);
            //use and store the conversion
            var converted = Math.Round(quantity*exchangeRate,2);
            
            //view on swagger
            var result = new CurrencyExchange
            {
                From = from,
                To = to,
                Quantity = quantity,
                ExchangeRate = exchangeRate,
                Converted = converted
            };

            return Ok(result);
        }
    }
}
