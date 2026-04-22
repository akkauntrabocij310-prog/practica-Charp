using System;
using System.Text;
class Program
{
    static void Main()
    {
        Console.Write("Введите основную строку: ");
        StringBuilder sb = new StringBuilder(Console.ReadLine());
        Console.Write("Введите строку для вставки: ");
        string insertText = Console.ReadLine();
        int middle = sb.Length / 2;
        sb.Insert(middle, insertText);
        Console.WriteLine($"Результат: {sb}");
    }
}