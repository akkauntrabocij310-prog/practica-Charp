using System;
class Program
{
    static void Main()
    {
        const int size = 100;
        int[] array = new int[size];
        Random rand = new Random();
        Console.WriteLine("Формирование массива из 100 случайных чисел:");
        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(-50, 51);
            Console.Write($"{array[i],4} ");
            if ((i + 1) % 20 == 0) 
                Console.WriteLine();
        }
        Console.WriteLine("\n");
        Console.WriteLine("Сначала отрицательные числа, затем все остальные:");
        Console.Write("Отрицательные: ");
        bool hasNegative = false;
        for (int i = 0; i < size; i++)
        {
            if (array[i] < 0)
            {
                Console.Write($"{array[i]} ");
                hasNegative = true;
            }
        }
        if (!hasNegative)
            Console.Write("нет");
        Console.WriteLine();
        Console.Write("Остальные: ");
        bool hasOther = false;
        for (int i = 0; i < size; i++)
        {
            if (array[i] >= 0)
            {
                Console.Write($"{array[i]} ");
                hasOther = true;
            }
        }
        if (!hasOther)
            Console.Write("нет");
        Console.WriteLine("\n");
        Console.WriteLine("Сортировка массива по возрастанию...");
        BubbleSort(array);
        Console.WriteLine("Отсортированный массив:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{array[i],4} ");
            if ((i + 1) % 20 == 0)
                Console.WriteLine();
        }
        Console.WriteLine("\n");
        Console.Write("Введите число k для бинарного поиска: ");
        int k = Convert.ToInt32(Console.ReadLine());
        int index = BinarySearch(array, k);
        if (index != -1)
        {
            Console.WriteLine($"Число {k} найдено в позиции {index} (индекс с 0)");
        }
        else
        {
            Console.WriteLine($"Число {k} не найдено в массиве");
        }
    }
    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }
    static int BinarySearch(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (arr[mid] == target)
                return mid;
            if (arr[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }
}