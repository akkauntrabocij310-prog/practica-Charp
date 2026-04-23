public partial class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }

    public Car(string brand, string model, int year, int mileage)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Mileage = mileage;
    }
}