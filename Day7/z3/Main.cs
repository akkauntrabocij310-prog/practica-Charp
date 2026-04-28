using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Проверка строки на пустое значение ===\n");
        StringProcessor processor = new StringProcessor();
        TestStringValidation(processor, "Hello World");
        Console.WriteLine();
        TestStringValidation(processor, "");
        Console.WriteLine();
       TestStringValidation(processor, null);
        Console.WriteLine();
        TestTrimmedValidation(processor, "   ");
        Console.WriteLine();
        TestStringValidation(processor, "   Valid Text   ");
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    static void TestStringValidation(StringProcessor processor, string input)
    {
        Console.WriteLine($"Входные данные: {(input == null ? "null" : $"\"{input}\"")}");

        try
        {
            string result = processor.ValidateInput(input);
            Console.WriteLine($"✓ Успех: {result}");
        }
        catch (EmptyStringException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ Ошибка: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine($"  Тип исключения: {ex.GetType().Name}");
            Console.WriteLine($"  Стек вызовов: {ex.StackTrace}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"✗ Неожиданная ошибка: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("  --- Завершение проверки ---");
        }
    }
    static void TestTrimmedValidation(StringProcessor processor, string input)
    {
        Console.WriteLine($"Входные данные (проверка с Trim): \"{input}\"");

        try
        {
            string result = processor.ValidateInputTrimmed(input);
            Console.WriteLine($"✓ Успех: \"{result}\"");
        }
        catch (EmptyStringException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ Ошибка: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("  --- Завершение проверки ---");
        }
    }
}