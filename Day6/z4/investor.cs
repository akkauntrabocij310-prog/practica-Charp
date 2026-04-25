using StockMonitoring;
public class Investor
{
    public string Name { get; set; }
    public void OnPriceChanged(object sender, StockEventArgs e)
    {
        if (e.Price < 150)
            Console.WriteLine($"[Инвестор {Name}] Цена {e.Symbol} низкая. Покупаю!");
        else
            Console.WriteLine($"[Инвестор {Name}] Цена {e.Symbol} высока. Держу позиции.");
    }
}
public class NewsPublisher
{
    public void OnPriceChanged(object sender, StockEventArgs e)
    {
        Console.WriteLine($"[News] СРОЧНО: Акции {e.Symbol} торгуются по цене {e.Price}$!");
    }
}