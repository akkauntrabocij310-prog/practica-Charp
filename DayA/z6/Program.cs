using System;

class Program
{
    static void Main()
    {
        double x = 0.7;
        double ex = Math.Exp(x);
        double cosEx = Math.Cos(ex);
        double lnCosEx = Math.Log(cosEx);
        double sinx = Math.Sin(x);
        double sin3x = Math.Pow(sinx, 3);
        double absPart = Math.Abs(1 - x * x);
        double denominator = Math.Sqrt(sin3x + absPart);

        double y = 20 * lnCosEx - 2 / denominator;

        Console.WriteLine($"x = {x}");
        Console.WriteLine($"y = {y:F6}");

        Console.ReadKey();
    }
}