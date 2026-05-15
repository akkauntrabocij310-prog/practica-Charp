using GameFactory.Models.Characters;

namespace GameFactory.Models.Factories
{
    /// <summary>
    /// Конкретная фабрика - создает Воинов
    /// </summary>
    public class WarriorFactory : CharacterFactory
    {
        public override ICharacter CreateCharacter(string name, int level)
        {
            return new Warrior(name, level);
        }

        public override string GetCharacterType()
        {
            return "Воин";
        }
    }
}