using TemperatureControl;
class Program
{
    static void Main()
    {
        TemperatureSensor sensor = new TemperatureSensor();
        CoolingSystem cooler = new CoolingSystem();
        WarningSystem warner = new WarningSystem();
        sensor.TemperatureChanged += cooler.OnTemperatureChanged;
        sensor.TemperatureChanged += warner.OnTemperatureChanged;
        sensor.SetTemperature(22.0);
        sensor.SetTemperature(28.0); 
        sensor.SetTemperature(42.0); 
        Console.WriteLine("\n--- Отключаем систему охлаждения для обслуживания ---");
        sensor.TemperatureChanged -= cooler.OnTemperatureChanged;
        sensor.SetTemperature(35.0); 
        Console.ReadKey();
    }
}