using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите координату x: ");
        double x = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите координату y: ");
        double y = Convert.ToDouble(Console.ReadLine());
        bool result = (x > 0 && y > 0) || (x < 0 && y < 0);

        Console.WriteLine($"Точка ({x}; {y}) лежит в 1 или 3 четверти: {result}");
    }
}