using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите начало отрезка A: ");
        double A = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите конец отрезка B: ");
        double B = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите количество точек M: ");
        int M = Convert.ToInt32(Console.ReadLine());
        double H = (B - A) / M;
        Console.WriteLine("\nРезультаты табулирования функции F(x):");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("    x    \t|\t   F(x)");
        Console.WriteLine("----------------------------------------");
        double x = A;
        for (int i = 1; i <= M; i++)
        {
            double y = CalculateFunction(x);
            Console.WriteLine($"{x,8:F4}\t|\t{y,12:F6}");
            x = x + H;
        }
        Console.WriteLine("----------------------------------------");
    }
    static double CalculateFunction(double x)
    {
        return x * x * Math.Exp(-x);
    }
}