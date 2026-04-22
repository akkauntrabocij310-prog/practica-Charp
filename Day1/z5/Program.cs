using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите целое число: ");
        int number = Convert.ToInt32(Console.ReadLine());
        bool isOdd = number % 2 != 0;
        if (isOdd)
        {
            Console.WriteLine($"Число {number} является нечетным");
        }
        else
        {
            Console.WriteLine($"Число {number} не является нечетным (оно четное)");
        }
    }
}