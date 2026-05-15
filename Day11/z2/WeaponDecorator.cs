namespace WeaponDecorator.Models.Decorators
{
    /// <summary>
    /// Абстрактный декоратор - базовый класс для всех улучшений
    /// </summary>
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon _weapon;

        protected WeaponDecorator(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public virtual string GetDescription()
        {
            return _weapon.GetDescription();
        }

        public virtual int GetDamage()
        {
            return _weapon.GetDamage();
        }

        public virtual int GetCost()
        {
            return _weapon.GetCost();
        }

        public virtual string GetRarity()
        {
            return _weapon.GetRarity();
        }
    }
}