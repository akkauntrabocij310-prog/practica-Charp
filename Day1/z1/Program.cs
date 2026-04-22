using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите массу в килограммах: ");
        double kilograms = Convert.ToDouble(Console.ReadLine());

        int tons = (int)(kilograms / 1000);

        Console.WriteLine($"Число полных тонн: {tons}");
    }
}