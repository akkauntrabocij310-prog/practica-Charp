using System;
class Program
{
    static void Main()
    {
        string test1 = "racecar";
        string test2 = "hello";
        string test3 = "А роза упала на лапу Азора";
        Console.WriteLine($"'{test1}' палиндром? {IsPalindrome(PrepareString(test1))}");
        Console.WriteLine($"'{test2}' палиндром? {IsPalindrome(PrepareString(test2))}");
        Console.WriteLine($"'{test3}' палиндром? {IsPalindrome(PrepareString(test3))}");
    }
    static string PrepareString(string s) => s.Replace(" ", "").ToLower();
    static bool IsPalindrome(string s)
    {
        if (s.Length <= 1)
            return true;
        if (s[0] != s[s.Length - 1])
            return false;
        return IsPalindrome(s.Substring(1, s.Length - 2));
    }
}