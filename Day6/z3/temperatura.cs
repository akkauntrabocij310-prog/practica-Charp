using System;
namespace TemperatureControl
{
    public delegate void TemperatureHandler(double temp);
    public class TemperatureSensor
    {
        public event TemperatureHandler TemperatureChanged;
        private double _currentTemperature;
        public void SetTemperature(double newTemp)
        {
            Console.WriteLine($"\n[Датчик] Текущая температура: {newTemp}°C");
            _currentTemperature = newTemp;
            if (TemperatureChanged != null)
            {
                TemperatureChanged(_currentTemperature);
            }
        }
    }
}