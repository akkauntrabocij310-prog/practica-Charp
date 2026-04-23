using System;
using System.Collections.Generic;
using System.Linq;
public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
public static class PersonProcessor
{
    public static Person[] GenerateData()
    {
        return new Person[]
        {
            new Person { Name = "Павел", Age = 34 },
            new Person { Name = "Анна", Age = 25 },
            new Person { Name = "Дмитрий", Age = 42 },
            new Person { Name = "Елена", Age = 19 }
        };
    }
    public static Person[] FilterByAge(Person[] people)
    {
        return people.Where(p => p.Age > 30).ToArray();
    }
    public static Person[] SortByName(Person[] people)
    {
        return people.OrderBy(p => p.Name).ToArray();
    }
    public static double GetAverageAge(Person[] people)
    {
        return people.Length == 0 ? 0 : people.Average(p => p.Age);
    }
}
class Program
{
    static void Main()
    {
        var group = PersonProcessor.GenerateData();
        var seniors = PersonProcessor.FilterByAge(group);
        Console.WriteLine("Люди старше 30 лет:");
        foreach (var p in seniors)
        {
            Console.WriteLine($"{p.Name} — {p.Age} лет");
        }
        Console.WriteLine($"\nСредний возраст в группе: {PersonProcessor.GetAverageAge(group)}");
    }
}