using GameFactory.Models.Characters;

namespace GameFactory.Models.Factories
{
    /// <summary>
    /// Конкретная фабрика - создает Лучников
    /// </summary>
    public class ArcherFactory : CharacterFactory
    {
        public override ICharacter CreateCharacter(string name, int level)
        {
            return new Archer(name, level);
        }

        public override string GetCharacterType()
        {
            return "Лучник";
        }
    }
}