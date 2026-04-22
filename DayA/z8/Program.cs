using System;
class Program
{
    static void Main()
    {
        double x = 4.3;
        double beta = 25;
        double underSqrt = beta - x * x;

        if (underSqrt < 0)
        {
            Console.WriteLine($"Ошибка: B - x² = {underSqrt} < 0, корень не определён!");
        }
        else if (Math.Abs(Math.Atan(x)) < 1e-10)
        {
            Console.WriteLine("Ошибка: arctg(x) = 0, деление на ноль!");
        }
        else
        {
            double sqrtPart = Math.Sqrt(underSqrt);
            double numerator = 1 + sqrtPart;
            double denominator = Math.Atan(x);
            double firstPart = numerator / denominator;

            double sinSqrtX = Math.Sin(Math.Sqrt(x));
            double secondPart = Math.Exp(sinSqrtX); 
            double y = firstPart - secondPart;

            Console.WriteLine($"x = {x}");
            Console.WriteLine($"B = {beta}");
            Console.WriteLine($"y = {y:F6}");
        }
        Console.ReadKey();
    }
}