using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Вычисление площади поверхности цилиндра");
        Console.WriteLine("Введите исходные данные:");

        Console.Write("Радиус основания: ");
        double radius = Convert.ToDouble(Console.ReadLine());

        Console.Write("Высота цилиндра: ");
        double height = Convert.ToDouble(Console.ReadLine());

        double surfaceArea = 2 * Math.PI * radius * (radius + height);

        Console.WriteLine($"Площадь поверхности: {surfaceArea:F2} кв. см.");

        Console.WriteLine("Для продолжения нажмите любую клавишу . . .");
        Console.ReadKey();
    }
}