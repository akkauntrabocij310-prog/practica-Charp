using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Число справа налево: {a % 10}{(a / 10) % 10}{a / 100}");

        Console.ReadKey();
    }
}