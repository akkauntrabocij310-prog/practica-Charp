using System;
class Program
{
    static void Main()
    {
        double[] triangleSides = { 3.0, 6.5, 12.0 };
        Console.WriteLine("Расчет параметров равносторонних треугольников:");
        Console.WriteLine("---------------------------------------------");
        foreach (double side in triangleSides)
        {
            double P, S;
            TrianglePS(side, out P, out S);
            Console.WriteLine($"Сторона: {side:F2}");
            Console.WriteLine($"Периметр: {P:F2}");
            Console.WriteLine($"Площадь: {S:F2}");
            Console.WriteLine("---------------------------------------------");
        }
    }
    static void TrianglePS(double a, out double p, out double s)
    {
        p = 3 * a;
        s = (Math.Sqrt(3) / 4) * Math.Pow(a, 2);
    }
}