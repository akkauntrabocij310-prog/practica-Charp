using System;
class Program
{
    static void Main()
    {
        int[] years = { 2000, 2024, 2010, 2025, 2026 };
        Console.WriteLine("Проверка годов на високосность:");
        Console.WriteLine("-------------------------------");
        foreach (int y in years)
        {
            bool result = IsLeapYear(y);
            string status = result ? "високосный" : "не високосный";
            Console.WriteLine($"{y} год — {status}");
        }
    }
    static bool IsLeapYear(int y)
    {
        return (y % 4 == 0 && y % 100 != 0) || (y % 400 == 0);
    }
}