using System;
using System.Linq;
public static class StringHelper
{
    public static bool IsPalindrome(string str)
    {
        if (string.IsNullOrEmpty(str)) return false;
        string cleaned = str.ToLower();
        string reversed = new string(cleaned.ToCharArray().Reverse().ToArray());

        return cleaned == reversed;
    }
}
class Program
{
    static void Main()
    {
        string word1 = "Довод";
        string word2 = "Программа";
        Console.WriteLine($"{word1} — палиндром? {StringHelper.IsPalindrome(word1)}");
        Console.WriteLine($"{word2} — палиндром? {StringHelper.IsPalindrome(word2)}");
    }
}