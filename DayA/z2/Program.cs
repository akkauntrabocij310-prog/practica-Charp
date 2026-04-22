using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Первая цифра: {a / 100}");
        Console.WriteLine($"Последняя цифра: {a % 10}");

        Console.ReadKey();
    }
}