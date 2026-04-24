using System;

namespace SwapExample
{
    public class Swapper
    {
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;  
            a = b;         
            b = temp;      
        }
        public static void Swap(ref double a, ref double b)
        {
            double temp = a; 
            a = b;           
            b = temp;        
        }
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Тестирование Swap с int ===");
            int x = 5;
            int y = 10;
            Console.WriteLine($"До Swap: x = {x}, y = {y}");
            Swapper.Swap(ref x, ref y);
            Console.WriteLine($"После Swap: x = {x}, y = {y}");
            Console.WriteLine("\n=== Тестирование Swap с double ===");
            double a = 2.5;
            double b = 5.0;
            Console.WriteLine($"До Swap: a = {a}, b = {b}");
            Swapper.Swap(ref a, ref b);
            Console.WriteLine($"После Swap: a = {a}, b = {b}");
            Console.WriteLine("\n=== Дополнительные тесты ===");
            int x2 = -7;
            int y2 = 3;
            Console.WriteLine($"До Swap: x2 = {x2}, y2 = {y2}");
            Swapper.Swap(ref x2, ref y2);
            Console.WriteLine($"После Swap: x2 = {x2}, y2 = {y2}");
            double a2 = 4.5;
            double b2 = 4.5;
            Console.WriteLine($"\nДо Swap: a2 = {a2}, b2 = {b2}");
            Swapper.Swap(ref a2, ref b2);
            Console.WriteLine($"После Swap: a2 = {a2}, b2 = {b2}");
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}