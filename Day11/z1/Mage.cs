using System;

namespace GameFactory.Models.Characters
{
    /// <summary>
    /// Конкретный класс продукта - Маг
    /// </summary>
    public class Mage : ICharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }

        private int _mana;

        public Mage(string name, int level)
        {
            Name = name;
            Level = level;
            _mana = level * 20;
        }

        public void Attack()
        {
            int damage = 15 + Level * 3;
            Console.WriteLine($" {Name} (Маг) колдует огненный шар! Наносит {damage} урона.");
            _mana -= 10;
        }

        public void SpecialAbility()
        {
            int healAmount = 20 + Level * 2;
            Console.WriteLine($" {Name} использует магическое восстановление! Восстанавливает {healAmount} HP.");
        }

        public string GetInfo()
        {
            return $" Маг: {Name} | Уровень: {Level} | Мана: {_mana} | Сила магии: {15 + Level * 3}";
        }
    }
}