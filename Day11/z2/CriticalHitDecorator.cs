using System;

namespace WeaponDecorator.Models.Decorators
{
    /// <summary>
    /// Конкретный декоратор - добавляет шанс критического удара
    /// </summary>
    public class CriticalHitDecorator : WeaponDecorator
    {
        private int _critChance;
        private double _critMultiplier;

        public CriticalHitDecorator(IWeapon weapon, int critChance = 25, double critMultiplier = 2.0)
            : base(weapon)
        {
            _critChance = critChance;
            _critMultiplier = critMultiplier;
        }

        public override string GetDescription()
        {
            return $"{_weapon.GetDescription()} +  Критический удар ({_critChance}% шанс, x{_critMultiplier} урон)";
        }

        public override int GetDamage()
        {
            return _weapon.GetDamage();
        }

        public override int GetCost()
        {
            return _weapon.GetCost() + 600;
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

        public int CalculateCriticalDamage(int baseDamage, Random random)
        {
            if (random.Next(100) < _critChance)
            {
                int critDamage = (int)(baseDamage * _critMultiplier);
                Console.WriteLine($"   КРИТИЧЕСКИЙ УДАР! {baseDamage} -> {critDamage} урона!");
                return critDamage;
            }
            return baseDamage;
        }

        public int GetCritChance() => _critChance;
    }
}