using System;
public abstract class Vehicle
{
    public abstract void Move();
    public virtual void Stop()
    {
        Console.WriteLine("Транспортное средство останавливается...");
    }
}
public class Car : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Машина едет");
    }
    public override void Stop()
    {
        Console.WriteLine("Машина останавливается при нажатии на педаль тормоза.");
    }
}
public class Bicycle : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Велосипед едет");
    }
    public override void Stop()
    {
        Console.WriteLine("Велосипед останавливается с помощью ручных тормозов.");
    }
}
class Program
{
    static void Main()
    {
        Vehicle[] garage = { new Car(), new Bicycle() };
        foreach (var vehicle in garage)
        {
            vehicle.Move();
            vehicle.Stop();
            Console.WriteLine();
        }
    }
}