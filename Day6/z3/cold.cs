public class CoolingSystem
{
    public void OnTemperatureChanged(double temp)
    {
        if (temp > 25.0)
        {
            Console.WriteLine("[CoolingSystem] СЛИШКОМ ЖАРКО! Включаю кондиционеры.");
        }
        else
        {
            Console.WriteLine("[CoolingSystem] Температура в норме. Кондиционеры выключены.");
        }
    }
}
public class WarningSystem
{
    public void OnTemperatureChanged(double temp)
    {
        if (temp > 40.0)
        {
            Console.WriteLine("[WarningSystem] КРИТИЧЕСКИЙ ПЕРЕГРЕВ! Отправка уведомления пожарной службе!");
        }
        else if (temp > 30.0)
        {
            Console.WriteLine("[WarningSystem] Внимание: Зафиксирована повышенная температура.");
        }
    }
}