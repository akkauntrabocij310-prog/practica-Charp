using StockMonitoring;
public class StockMarket
{
    public event EventHandler<StockEventArgs> StockPriceUpdated;
    public void UpdateQuote(string symbol, double price)
    {
        Console.WriteLine($"\n[Биржа] Котировка {symbol} изменилась: {price}$");
        StockPriceUpdated?.Invoke(this, new StockEventArgs(symbol, price));
    }
}