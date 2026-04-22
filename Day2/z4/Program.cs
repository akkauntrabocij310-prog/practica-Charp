using System;
class Program
{
    static void Main()
    {
        int[,] salary = new int[18, 12];
        Random rnd = new Random();
        for (int i = 0; i < 18; i++)
            for (int j = 0; j < 12; j++)
                salary[i, j] = rnd.Next(20000, 150000);
        int total = 0;
        for (int j = 0; j < 12; j++)
            total += salary[0, j];
        Console.Write("Введите число: ");
        int num = Convert.ToInt32(Console.ReadLine());

        if (total > num)
            Console.WriteLine("Верно, годовой доход первого человека больше " + num);
        else
            Console.WriteLine("Не верно, годовой доход первого человека не больше " + num);
    }
}