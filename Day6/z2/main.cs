using DelegateParameters;
class Program
{
    static void Main()
    {
        int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Console.WriteLine("Исходный массив: " + string.Join(", ", data));
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Фильтрация четных чисел:");
        ArrayProcessor.FilterNumbers(data, NumberFilters.IsEven);
        Console.WriteLine("\nФильтрация нечетных чисел:");
        ArrayProcessor.FilterNumbers(data, NumberFilters.IsOdd);
        Console.WriteLine("\nФильтрация чисел больше 5 (через лямбду):");
        ArrayProcessor.FilterNumbers(data, n => n > 5);
        Console.ReadKey();
    }
}