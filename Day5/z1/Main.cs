class Program
{
    static void Main()
    {
        Shape[] shapes = new Shape[]
        {
            new Circle(5),
            new Rectangle(10, 4),
            new Triangle(6, 8)
        };
        Console.WriteLine("Расчет площадей фигур:");
        Console.WriteLine("-----------------------");
        double totalArea = 0;
        foreach (var shape in shapes)
        {
            double area = shape.GetArea();
            totalArea += area;
            Console.WriteLine($"Фигура: {shape.GetType().Name,-10} | Площадь: {area:F2}");
        }
        Console.WriteLine("-----------------------");
        Console.WriteLine($"Общая площадь всех фигур: {totalArea:F2}");
    }
}