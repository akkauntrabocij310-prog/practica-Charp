namespace WeaponDecorator.Models
{
    /// <summary>
    /// Интерфейс компонента - определяет общий интерфейс для оружия
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        /// Получить описание оружия
        /// </summary>
        string GetDescription();

        /// <summary>
        /// Получить урон оружия
        /// </summary>
        int GetDamage();

        /// <summary>
        /// Получить стоимость оружия
        /// </summary>
        int GetCost();

        /// <summary>
        /// Получить редкость оружия
        /// </summary>
        string GetRarity();
    }
}