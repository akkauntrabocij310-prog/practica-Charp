using System;

namespace GameFactory.Models.Characters
{
    /// <summary>
    /// Конкретный класс продукта - Лучник
    /// </summary>
    public class Archer : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }

        private int _arrows;

        public Archer(string name, int level)
        {
            Name = name;
            Level = level;
            _arrows = 20 + level * 2;
        }

        public void Attack()
        {
            int damage = 12 + Level * 4;
            _arrows--;
            Console.WriteLine($" {Name} (Лучник) выпускает точную стрелу! Наносит {damage} урона. Осталось стрел: {_arrows}");
        }

        public void SpecialAbility()
        {
            int critChance = 30 + Level;
            Console.WriteLine($" {Name} использует Снайперский выстрел! Шанс крита: {critChance}%. Пробивает броню цели.");
        }

        public string GetInfo()
        {
            return $" Лучник: {Name} | Уровень: {Level} | Стрелы: {_arrows} | Точность: {12 + Level * 4}";
        }
    }
}