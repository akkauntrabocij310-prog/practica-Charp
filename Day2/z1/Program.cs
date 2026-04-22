using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов массива: ");
        int size = Convert.ToInt32(Console.ReadLine());
        int[] array = new int[size];
        Console.WriteLine("Введите элементы массива:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"array[{i}] = ");
            array[i] = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("\nПорядковые номера нечетных элементов:");
        Console.WriteLine("(нумерация с 1)");
        bool found = false;
        for (int i = 0; i < size; i++)
        {
            if (array[i] % 2 != 0) 
            {
                Console.Write($"{i + 1} ");
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Нечетных элементов нет");
        }
        Console.WriteLine();
    }
}
