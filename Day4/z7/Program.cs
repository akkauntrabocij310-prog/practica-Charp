using System;

public class AnimalSounds
{
    public static string GetAnimalSound(string animal)
    {
        switch (animal)
        {
            case "Dog":
                return "Woof";
            case "Cat":
                return "Meow";
            case "Cow":
                return "Moo";
            default:
                return "Unknown animal";
        }
    }
    public static string GetAnimalSound(string animal, string noise)
    {
        return noise;
    }
}
public class AnimalSoundsExtended
{
    private static readonly Dictionary<string, string> DefaultSounds = new Dictionary<string, string>
    {
        { "Dog", "Woof" },
        { "Cat", "Meow" },
        { "Cow", "Moo" }
    };
    public static string GetAnimalSound(string animal)
    {
        return DefaultSounds.TryGetValue(animal, out string sound) ? sound : "Unknown animal";
    }
    public static string GetAnimalSound(string animal, string noise)
    {
        return noise;
    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine(AnimalSounds.GetAnimalSound("Dog"));
        Console.WriteLine(AnimalSounds.GetAnimalSound("Cat")); 
        Console.WriteLine(AnimalSounds.GetAnimalSound("Cow"));
        Console.WriteLine(AnimalSounds.GetAnimalSound("Dog", "Bark"));     
        Console.WriteLine(AnimalSounds.GetAnimalSound("Cat", "Purr"));     
        Console.WriteLine(AnimalSounds.GetAnimalSound("Cow", "Moooo"));
    }
}