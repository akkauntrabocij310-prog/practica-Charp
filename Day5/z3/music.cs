using System;
using System.Linq;
using System.Collections.Generic;
class Instrument
{
    public string Name { get; set; }
    public Instrument(string name) => Name = name;
    public virtual void Play() => Console.WriteLine($"Играет инструмент {Name}");
}
interface IStringInstrument
{
    int StringCount { get; set; }
    void TuneStrings();
}
interface IPercussionInstrument
{
    bool HasDrumsticks { get; set; }
    void Hit();
}
class Guitar : Instrument, IStringInstrument
{
    public int StringCount { get; set; } = 6;
    public Guitar(string name) : base(name) { }
    public void TuneStrings() => Console.WriteLine($"{Name}: Струны настроены.");
    public override void Play() => Console.WriteLine($"{Name} издает мелодичный звук.");
}
class Drum : Instrument, IPercussionInstrument
{
    public bool HasDrumsticks { get; set; } = true;
    public Drum(string name) : base(name) { }
    public void Hit() => Console.WriteLine($"{Name}: Бум!");
    public override void Play() => Console.WriteLine($"{Name} задает ритм.");
}