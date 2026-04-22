using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите значение x: ");
        double x = Convert.ToDouble(Console.ReadLine());
        double y;
        if (x < 1.3)
        {
            y = Math.PI * Math.Pow(x, 2) - 7 / Math.Sqrt(Math.Abs(x));
        }
        else
        {
            y = 3 * x - Math.Pow(Math.Cos(x), 2);
        }
        Console.WriteLine($"При x = {x}");
        Console.WriteLine($"y = {y:F4}");
    }
}