using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string str = Console.ReadLine();
        Console.Write("Введите подстроку для поиска: ");
        string sub = Console.ReadLine();
        int index = -1;
        for (int i = 0; i <= str.Length - sub.Length; i++)
        {
            bool found = true;
            for (int j = 0; j < sub.Length; j++)
            {
                if (str[i + j] != sub[j])
                {
                    found = false;
                    break;
                }
            }
            if (found)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
            Console.WriteLine($"Первое вхождение подстроки на индексе: {index}");
        else
            Console.WriteLine("Подстрока не найдена");
    }
}