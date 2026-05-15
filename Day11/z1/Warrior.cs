using System;

namespace GameFactory.Models.Characters
{
    /// <summary>
    /// Конкретный класс продукта - Воин
    /// </summary>
    public class Warrior : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }

        private int _rage;

        public Warrior(string name, int level)
        {
            Name = name;
            Level = level;
            _rage = 0;
        }

        public void Attack()
        {
            int damage = 20 + Level * 5;
            Console.WriteLine($" {Name} (Воин) наносит мощный удар мечом! Наносит {damage} урона.");
            _rage += 15;
        }

        public void SpecialAbility()
        {
            Console.WriteLine($" {Name} использует Боевой клич! Повышает атаку всех союзников на 20% на 3 хода.");
        }

        public string GetInfo()
        {
            return $" Воин: {Name} | Уровень: {Level} | Ярость: {_rage} | Сила удара: {20 + Level * 5}";
        }
    }
}