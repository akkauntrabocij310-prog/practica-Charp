using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string str = Console.ReadLine();
        if (string.IsNullOrEmpty(str))
        {
            Console.WriteLine("Строка пуста");
            return;
        }
        char maxChar = str[0];
        int maxCount = 1;
        int currentCount = 1;
        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] == str[i - 1])
            {
                currentCount++;
            }
            else
            {
                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                    maxChar = str[i - 1];
                }
                currentCount = 1;
            }
        }
        if (currentCount > maxCount)
        {
            maxCount = currentCount;
            maxChar = str[str.Length - 1];
        }
        Console.WriteLine($"Самая длинная последовательность: '{maxChar}' повторяется {maxCount} раз");
    }
}