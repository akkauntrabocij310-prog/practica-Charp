using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите целое число N (1 <= N <= 10): ");
        int N = Convert.ToInt32(Console.ReadLine());
        if (N < 1 || N > 10)
        {
            Console.WriteLine("Ошибка: N должно быть от 1 до 10");
            return;
        }
        int sum = 0;
        Console.WriteLine($"Вычисление квадрата числа {N} по формуле:");
        Console.WriteLine("N^2 = 1 + 3 + 5 + ... + (2·N – 1)");
        Console.WriteLine();
        for (int i = 1; i <= N; i++)
        {
            int oddNumber = 2 * i - 1;
            sum += oddNumber;
            Console.WriteLine($"После добавления {oddNumber,2} сумма = {sum,3} (это {i}^2)");
        }
        Console.WriteLine();
        Console.WriteLine($"Квадрат числа {N} = {sum}");
    }
}