using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите целое число n: ");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите цифру k (0-9): ");
        int k = Convert.ToInt32(Console.ReadLine());
        if (k < 0 || k > 9)
        {
            Console.WriteLine("Ошибка: k должно быть цифрой от 0 до 9");
            return;
        }
        int count = 0;
        int temp = Math.Abs(n);
        if (temp == 0)
        {
            if (k == 0)
                count = 1;
        }
        else
        {
            while (temp > 0)
            {
                int digit = temp % 10;
                if (digit == k)
                    count++;
                temp /= 10;
            }
        }
        Console.WriteLine($"Цифра {k} встречается в числе {n} {count} раз(а)");
    }
}