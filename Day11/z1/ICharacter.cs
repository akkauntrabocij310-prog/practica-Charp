namespace GameFactory.Models.Characters
{
    /// <summary>
    /// Интерфейс продукта - определяет общий интерфейс всех персонажей
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// Имя персонажа
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Уровень персонажа
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// Атака персонажа
        /// </summary>
        void Attack();

        /// <summary>
        /// Получить информацию о персонаже
        /// </summary>
        string GetInfo();

        /// <summary>
        /// Специальная способность
        /// </summary>
        void SpecialAbility();
    }
}