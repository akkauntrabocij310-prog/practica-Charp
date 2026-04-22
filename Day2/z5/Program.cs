using System;
class Program
{
    static void Main()
    {
        int[][] arr = new int[5][];
        Random rnd = new Random();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = new int[rnd.Next(3, 7)];
            for (int j = 0; j < arr[i].Length; j++)
            {
                arr[i][j] = rnd.Next(1, 20);
            }
        }
        Console.WriteLine("Исходный массив:");
        PrintArray(arr);
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int k = 0; k < arr.Length - 1 - i; k++)
            {
                int sum1 = SumRow(arr[k]);
                int sum2 = SumRow(arr[k + 1]);
                if (sum1 < sum2)
                { 
                    int[] temp = arr[k];
                    arr[k] = arr[k + 1];
                    arr[k + 1] = temp;
                }
            }
        }
        Console.WriteLine("\nМассив после сортировки (по убыванию суммы строк):");
        PrintArray(arr);
    }
    static int SumRow(int[] row)
    {
        int sum = 0;
        for (int i = 0; i < row.Length; i++)
            sum += row[i];
        return sum;
    }
    static void PrintArray(int[][] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write($"Строка {i}: сумма = {SumRow(arr[i])} -> ");
            for (int j = 0; j < arr[i].Length; j++)
            {
                Console.Write(arr[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
}