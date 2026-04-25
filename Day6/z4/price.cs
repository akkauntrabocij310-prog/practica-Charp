using System;

namespace StockMonitoring
{
    public class StockEventArgs : EventArgs
    {
        public string Symbol { get; }
        public double Price { get; }
        public StockEventArgs(string symbol, double price)
        {
            Symbol = symbol;
            Price = price;
        }
    }
}