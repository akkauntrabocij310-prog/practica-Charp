using GameFactory.Models.Characters;

namespace GameFactory.Models.Factories
{
    /// <summary>
    /// Абстрактная фабрика - определяет метод создания персонажей
    /// </summary>
    public abstract class CharacterFactory
    {
        /// <summary>
        /// Фабричный метод для создания персонажа
        /// </summary>
        public abstract ICharacter CreateCharacter(string name, int level);

        /// <summary>
        /// Создание персонажа с уровнем по умолчанию
        /// </summary>
        public ICharacter CreateCharacter(string name)
        {
            return CreateCharacter(name, 1);
        }

        /// <summary>
        /// Получить тип создаваемого персонажа
        /// </summary>
        public abstract string GetCharacterType();
    }
}