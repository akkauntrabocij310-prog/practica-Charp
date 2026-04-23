using System;
using System.Linq;
using System.Collections.Generic;
public class Fleet
{
    public Car[] Cars { get; set; }
    public Fleet(Car[] cars)
    {
        Cars = cars;
    }
    public Car[] GetNewestCars(int year)
    {
        if (Cars == null) return Array.Empty<Car>();
        return Cars.Where(c => c.Year > year).ToArray();
    }
    public Car[] GetCarsByBrand(string brand)
    {
        if (Cars == null) return Array.Empty<Car>();
        return Cars.Where(c => c.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToArray();
    }
}