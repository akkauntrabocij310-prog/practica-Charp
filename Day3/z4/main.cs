class Program
{
    static void Main()
    {
        var myFleet = new Fleet(new Car[]
        {
            new Car("Toyota", "Camry", 2021, 15000),
            new Car("BMW", "M5", 2018, 45000),
            new Car("Tesla", "Model 3", 2023, 5000),
            new Car("Toyota", "RAV4", 2015, 80000)
        });
        Console.WriteLine("Автомобили после 2020 года:");
        var newCars = myFleet.GetNewestCars(2020);
        foreach (var car in newCars) car.DisplayInfo();
        Console.WriteLine("\nАвтомобили марки Toyota:");
        var toyotas = myFleet.GetCarsByBrand("Toyota");
        foreach (var car in toyotas) car.DisplayInfo();
    }
}