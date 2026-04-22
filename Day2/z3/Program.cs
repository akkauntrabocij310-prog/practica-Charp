using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите размер матрицы N (N < 10): ");
        int N = Convert.ToInt32(Console.ReadLine());
        if (N >= 10 || N <= 0)
        {
            Console.WriteLine("Ошибка: N должно быть от 1 до 9");
            return;
        }
        Console.Write("Введите a (начало диапазона): ");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите b (конец диапазона): ");
        int b = Convert.ToInt32(Console.ReadLine());
        if (a > b)
        {
            Console.WriteLine("Ошибка: a должно быть меньше или равно b");
            return;
        }
        Console.Write("Введите C (начало промежутка для произведения): ");
        int C = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите D (конец промежутка для произведения): ");
        int D = Convert.ToInt32(Console.ReadLine());
        int[,] matrix = new int[N, N];
        Random rand = new Random();
        Console.WriteLine("\nИсходная матрица:");
        Console.WriteLine(new string('-', N * 8));
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = rand.Next(a, b + 1);
                Console.Write($"{matrix[i, j],6} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', N * 8));
        long product = 1;
        bool hasNumbersInRange = false;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (matrix[i, j] >= C && matrix[i, j] <= D)
                {
                    product *= matrix[i, j];
                    hasNumbersInRange = true;
                }
            }
        }
        Console.WriteLine("\nРезультаты вычислений:");
        Console.WriteLine("----------------------------------------");
        if (hasNumbersInRange)
        {
            Console.WriteLine($"Произведение чисел из промежутка [{C}, {D}]: {product}");
        }
        else
        {
            Console.WriteLine($"В матрице нет чисел из промежутка [{C}, {D}]");
        }
        Console.WriteLine("\nСумма элементов каждого столбца:");
        for (int j = 0; j < N; j++)
        {
            int columnSum = 0;
            for (int i = 0; i < N; i++)
            {
                columnSum += matrix[i, j];
            }
            Console.WriteLine($"Столбец {j + 1}: сумма = {columnSum}");
        }
        Console.WriteLine("----------------------------------------");
    }
}