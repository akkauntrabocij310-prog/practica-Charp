using GameFactory.Models.Characters;

namespace GameFactory.Models.Factories
{
    /// <summary>
    /// Конкретная фабрика - создает Магов
    /// </summary>
    public class MageFactory : CharacterFactory
    {
        public override ICharacter CreateCharacter(string name, int level)
        {
            return new Mage(name, level);
        }

        public override string GetCharacterType()
        {
            return "Маг";
        }
    }
}