using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите число A: ");
        int A = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите число B: ");
        int B = Convert.ToInt32(Console.ReadLine());
        if (A >= B)
        {
            Console.WriteLine("Ошибка: A должно быть меньше B");
            return;
        }
        int N = B - A - 1;
        Console.WriteLine($"Числа между {A} и {B} в порядке убывания:");
        for (int i = B - 1; i > A; i--)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        Console.WriteLine($"Количество чисел: {N}");
    }
}