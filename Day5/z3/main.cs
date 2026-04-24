class Program
{
    static void Main()
    {
        Instrument[] inventory = new Instrument[]
        {
            new Guitar("Акустическая гитара"),
            new Drum("Бас-бочка"),
            new Guitar("Электрогитара"),
            new Drum("Малый барабан"),
            new Instrument("Неизвестный предмет")
        };
        Console.WriteLine("--- Поиск струнных инструментов ---");
        foreach (var item in inventory)
        {
            if (item is IStringInstrument stringInst)
            {
                Console.WriteLine($"Найдено: {item.Name}");
                stringInst.TuneStrings();
                item.Play();              
                Console.WriteLine();
            }
        }
    }
}