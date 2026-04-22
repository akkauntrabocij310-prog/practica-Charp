using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string text = Console.ReadLine();
        Regex regex = new Regex(@"\b\d{2}\.\d{2}\.\d{4}\b");
        MatchCollection matches = regex.Matches(text);
        if (matches.Count > 0)
        {
            Console.WriteLine("\nНайденные даты:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        else
        {
            Console.WriteLine("Даты не найдены");
        }
    }
}