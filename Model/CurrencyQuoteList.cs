namespace CurencyCrunchActivity.Model
{
    public class CurrencyQuoteList
    {
        public List<string> Quotes { get; set; }
    }

    public class CurrencyExchange
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Quantity { get; set; }
        
        public decimal ExchangeRate { get; set; }
        public decimal Converted { get; set; }
    }
        
}
