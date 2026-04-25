using DelegateParameters;
public class ArrayProcessor
{
    public static void FilterNumbers(int[] numbers, NumberCheck check)
    {
        Console.Write("Результат фильтрации: ");
        foreach (int num in numbers)
        {
            if (check(num))
            {
                Console.Write($"{num} ");
            }
        }
        Console.WriteLine();
    }
}