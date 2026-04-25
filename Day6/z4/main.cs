class Program
{
    static void Main()
    {
        StockMarket nasdaq = new StockMarket();
        MarketObserver observer = new MarketObserver();
        Investor warren = new Investor { Name = "Баффет" };
        NewsPublisher reuters = new NewsPublisher();
        observer.SetupSubscribers(nasdaq, warren, reuters);
        nasdaq.UpdateQuote("APPLE", 145.50);
        nasdaq.UpdateQuote("TESLA", 700.10); 
        Console.ReadKey();
    }
}