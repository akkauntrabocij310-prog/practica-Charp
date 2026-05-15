using System;

namespace WeaponDecorator.Models.Decorators
{
    /// <summary>
    /// Конкретный декоратор - добавляет ледяной эффект
    /// </summary>
    public class IceEnhancementDecorator : WeaponDecorator
    {
        private int _iceDamage;
        private int _slowChance;

        public IceEnhancementDecorator(IWeapon weapon, int iceDamage = 12, int slowChance = 40)
            : base(weapon)
        {
            _iceDamage = iceDamage;
            _slowChance = slowChance;
        }

        public override string GetDescription()
        {
            return $"{_weapon.GetDescription()} +  Ледяная атака (шанс замедления: {_slowChance}%)";
        }

        public override int GetDamage()
        {
            return _weapon.GetDamage() + _iceDamage;
        }

        public override int GetCost()
        {
            return _weapon.GetCost() + 450;
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

        public void ApplyIceEffect()
        {
            Console.WriteLine($"   Ледяной эффект активирован! Наносит {_iceDamage} урона и замедляет цель.");
        }
    }
}