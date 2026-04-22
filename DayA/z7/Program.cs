using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите длину окружности: ");
        double L = Convert.ToDouble(Console.ReadLine());
        double S = L * L / (4 * Math.PI);
        Console.WriteLine($"Площадь круга: {S:F2}");
        Console.ReadKey();
    }
}