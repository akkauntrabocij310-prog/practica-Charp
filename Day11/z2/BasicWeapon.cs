namespace WeaponDecorator.Models
{
    /// <summary>
    /// Конкретный компонент - базовое оружие
    /// </summary>
    public class BasicWeapon : IWeapon
    {
        private string _name;
        private int _damage;
        private int _cost;
        private string _rarity;

        public BasicWeapon(string name, int damage, int cost, string rarity = "Обычное")
        {
            _name = name;
            _damage = damage;
            _cost = cost;
            _rarity = rarity;
        }

        public string GetDescription()
        {
            return $"{_name}";
        }

        public int GetDamage()
        {
            return _damage;
        }

        public int GetCost()
        {
            return _cost;
        }

        public string GetRarity()
        {
            return _rarity;
        }
    }
}