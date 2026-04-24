using System;
public static class ArrayExtensions
{
    public static int TotalSum(this int[] array)
    {
        int sum = 0;
        foreach (int item in array)
        {
            sum += item;
        }
        return sum;
    }
}
class Program
{
    static void Main()
    {
        int[] numbers = { 10, 20, 30, 40 };
        int result = numbers.TotalSum();
        Console.WriteLine($"Сумма элементов массива: {result}");
    }
}