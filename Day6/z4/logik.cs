public class MarketObserver
{
    public void SetupSubscribers(StockMarket market, Investor investor, NewsPublisher news)
    {
        market.StockPriceUpdated += investor.OnPriceChanged;
        market.StockPriceUpdated += news.OnPriceChanged;
        Console.WriteLine("System: Связи между биржей и сервисами установлены.");
    }
}