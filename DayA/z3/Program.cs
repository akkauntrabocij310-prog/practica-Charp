using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите угол a в градусах: ");
        double a = Convert.ToDouble(Console.ReadLine()) * Math.PI / 180;

        double z1 = (Math.Sin(4 * a) / (1 + Math.Cos(4 * a))) * (Math.Cos(2 * a) / (1 + Math.Cos(2 * a)));
        double z2 = Math.Tan(a);

        Console.WriteLine($"z1 = {z1:F6}");
        Console.WriteLine($"z2 = {z2:F6}");

        Console.ReadKey();
    }
}