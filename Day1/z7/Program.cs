using System;
class Program
{
    static void Main()
    {
        const double poundsToKg = 0.453;
        Console.WriteLine("Таблица перевода фунтов в килограммы");
        Console.WriteLine("====================================");
        Console.WriteLine("Фунты\t|\tКилограммы");
        Console.WriteLine("------------------------------------");
        for (int pounds = 1; pounds <= 100; pounds++)
        {
            double kg = pounds * poundsToKg;
            Console.WriteLine($"{pounds}\t|\t{kg:F3}");
        }
        Console.WriteLine("====================================");
    }
}