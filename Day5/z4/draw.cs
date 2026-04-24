using System;
interface IDraw
{
    void ApplyColor(string color);
}
interface IPaint
{
    void ApplyColor(string color);
}
class GraphicEditor : IDraw, IPaint
{
    void IDraw.ApplyColor(string color)
    {
        Console.WriteLine($"[IDraw] Установлен цвет контура: {color}");
    }
    void IPaint.ApplyColor(string color)
    {
        Console.WriteLine($"[IPaint] Установлен цвет заливки: {color}");
    }
}