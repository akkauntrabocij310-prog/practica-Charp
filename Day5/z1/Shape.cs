using System;
abstract class Shape
{
    public abstract double GetArea();
}
class Circle : Shape
{
    public double Radius { get; set; }
    public Circle(double radius) => Radius = radius;
    public override double GetArea() => Math.PI * Math.Pow(Radius, 2);
}
class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    public override double GetArea() => Width * Height;
}
class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }
    public Triangle(double @base, double height)
    {
        Base = @base;
        Height = height;
    }
    public override double GetArea() => 0.5 * Base * Height;
}