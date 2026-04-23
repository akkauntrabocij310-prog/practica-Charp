public partial class Car
{
    public void DisplayInfo()
    {
        Console.WriteLine($"{Brand} {Model} ({Year}), Пробег: {Mileage} км");
    }
}