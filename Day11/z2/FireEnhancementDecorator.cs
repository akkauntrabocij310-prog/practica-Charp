using System;

namespace WeaponDecorator.Models.Decorators
{
    /// <summary>
    /// Конкретный декоратор - добавляет огненный эффект
    /// </summary>
    public class FireEnhancementDecorator : WeaponDecorator
    {
        private int _fireDamage;
        private int _burnChance;

        public FireEnhancementDecorator(IWeapon weapon, int fireDamage = 15, int burnChance = 30)
            : base(weapon)
        {
            _fireDamage = fireDamage;
            _burnChance = burnChance;
        }

        public override string GetDescription()
        {
            return $"{_weapon.GetDescription()} +  Огненная атака (шанс поджога: {_burnChance}%)";
        }

        public override int GetDamage()
        {
            return _weapon.GetDamage() + _fireDamage;
        }

        public override int GetCost()
        {
            return _weapon.GetCost() + 500;
        }

        public override string GetRarity()
        {
            string baseRarity = _weapon.GetRarity();
            return UpgradeRarity(baseRarity);
        }

        private string UpgradeRarity(string currentRarity)
        {
            var rarityLevels = new[] { "Обычное", "Необычное", "Редкое", "Эпическое", "Легендарное" };
            int currentIndex = Array.IndexOf(rarityLevels, currentRarity);

            if (currentIndex < rarityLevels.Length - 1)
                return rarityLevels[currentIndex + 1];

            return currentRarity;
        }

        public void ApplyFireEffect()
        {
            Console.WriteLine($"   Огненный эффект активирован! Наносит {_fireDamage} дополнительного урона огнём.");
        }
    }
}