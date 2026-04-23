using System;
class Program
{
    static void Main()
    {
        double a = 5;
        double b = 10;
        Console.WriteLine($"Произведение: {Multiply(a, b)}");
        Console.WriteLine($"Результат выражения: {CalculateExpression(a, b)}");
    }
    static double Multiply(double a, double b)
    {
        return a * b;
    }
    static double CalculateExpression(double a, double b)
    {
        return Math.Sqrt(b) / (2 * a);
    }
}