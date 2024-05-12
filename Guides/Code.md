# Code Steps

1. Create our model, you can use the endpoints to Use a JSON to C# converter and view the variables. Or review the code snippet from rapidAPI
2. Discover that  from, to and quantity is needed.
3. Create your model
4. Next step to establish the connection by building the client service.
5. In Program.cs add the following (**replacing the Key and Host with your**)
'''
builder.Services.AddHttpClient("CurrencyConverter", client =>
{
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Your key");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "Your host");

});
'''
6. Now we can create our endpoints
7. In your controller instantiate an IHttpClientFactory
'''
private readonly IHttpClientFactory _httpClientFactory;

 public CurrencyController(IHttpClientFactory httpClientFactory)
 {
 _httpClientFactory = httpClientFactory;
 }
 '''
 This will be used to esytablishg the connection.
 8. Now to complete the Client's request we only need to add the "exchange" controller and end point
 '''
 [HttpGet("exchange")]
        public async Task<IActionResult> GetExchangeRate(string from, string to, decimal quantity)
        {
            var client = _httpClientFactory.CreateClient("CurrencyConverter");
            var url = $"https://currency-exchange.p.rapidapi.com/exchange?from={from}&to={to}&q={quantity}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();           
            
            //view on swagger
            var result = new CurrencyExchange
            {
                From = from,
                To = to,
                Quantity = quantity
            };

            return Ok(result);
        }
'''
9. Execute to test **but** we are not done just yet
10. Proceed to add 2 more variables to the model, one to hold te exchange rate from the object and another the calculated conversion value
11. Ammend the "exchange" endpoint to display the information
12. Execute again and test.