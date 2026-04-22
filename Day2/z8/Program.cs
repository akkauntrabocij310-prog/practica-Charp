using System;
using System.Text;
class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string str = Console.ReadLine();
        string result = ToSnakeCase(str);
        Console.WriteLine($"Результат: {result}");
    }
    static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0)
                    sb.Append('_');
                sb.Append(char.ToLower(c));
            }
            else if (char.IsWhiteSpace(c))
            {
                sb.Append('_');
            }
            else
            {
                sb.Append(char.ToLower(c));
            }
        }
        return sb.ToString();
    }
}