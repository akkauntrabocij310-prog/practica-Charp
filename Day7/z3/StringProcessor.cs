using System;
public class StringProcessor
{
    public string ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new EmptyStringException(
                $"Ошибка: Входная строка {(input == null ? "null" : "пустая")}. " +
                "Пожалуйста, предоставьте непустую строку.");
        }
        string processedString = input.Trim().ToUpper();
        Console.WriteLine($"Строка успешно обработана. Результат: \"{processedString}\"");
        return processedString;
    }
    public string ValidateInputTrimmed(string input)
    {
        if (input == null || string.IsNullOrWhiteSpace(input))
        {
            throw new EmptyStringException(
                "Строка не может состоять только из пробелов или быть null.");
        }
        return input.Trim();
    }
}