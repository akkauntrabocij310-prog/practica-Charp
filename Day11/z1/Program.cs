using System;
using System.Collections.Generic;
using GameFactory.Models.Characters;
using GameFactory.Models.Factories;

namespace GameFactory
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Игра - Фабричный метод";

            PrintHeader("ПАТТЕРН ФАБРИЧНЫЙ МЕТОД");
            PrintDescription();

            // 1. Создание персонажей через фабрики
            DemonstrateFactoryMethod();

            // 2. Битва персонажей
            DemonstrateBattle();

            // 3. Динамическое создание персонажей
            DemonstrateDynamicCreation();

            Console.WriteLine("\n\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void DemonstrateFactoryMethod()
        {
            PrintPhase("СОЗДАНИЕ ПЕРСОНАЖЕЙ ЧЕРЕЗ ФАБРИКИ");

            // Создаем фабрики
            var mageFactory = new MageFactory();
            var warriorFactory = new WarriorFactory();
            var archerFactory = new ArcherFactory();

            // Создаем персонажей через фабрики
            var gandalf = mageFactory.CreateCharacter("Гэндальф", 10);
            var conan = warriorFactory.CreateCharacter("Конан", 8);
            var legolas = archerFactory.CreateCharacter("Леголас", 7);
            var newbieMage = mageFactory.CreateCharacter("Новичок Мерлин");

            // Выводим информацию
            Console.WriteLine($"\nСоздано персонажей: 4");
            Console.WriteLine($"Типы фабрик: {mageFactory.GetCharacterType()}, {warriorFactory.GetCharacterType()}, {archerFactory.GetCharacterType()}");

            Console.WriteLine($"\n{gandalf.GetInfo()}");
            Console.WriteLine($"{conan.GetInfo()}");
            Console.WriteLine($"{legolas.GetInfo()}");
            Console.WriteLine($"{newbieMage.GetInfo()}");
        }

        static void DemonstrateBattle()
        {
            PrintPhase("БИТВА ПЕРСОНАЖЕЙ");

            // Создаем команду через фабрики
            var factories = new List<CharacterFactory>
            {
                new MageFactory(),
                new WarriorFactory(),
                new ArcherFactory(),
                new MageFactory()
            };

            var team = new List<ICharacter>();
            string[] names = { "Аларик", "Тор", "Леголас", "Мерлин" };
            int[] levels = { 5, 7, 6, 8 };

            for (int i = 0; i < factories.Count; i++)
            {
                var character = factories[i].CreateCharacter(names[i], levels[i]);
                team.Add(character);
                Console.WriteLine($"\n{character.GetInfo()}");
            }

            Console.WriteLine("\n БИТВА НАЧИНАЕТСЯ! ");
            Console.WriteLine(new string('=', 50));

            foreach (var character in team)
            {
                character.Attack();
                System.Threading.Thread.Sleep(500);
            }

            Console.WriteLine("\n СПЕЦИАЛЬНЫЕ СПОСОБНОСТИ ");
            foreach (var character in team)
            {
                character.SpecialAbility();
                System.Threading.Thread.Sleep(500);
            }
        }

        static void DemonstrateDynamicCreation()
        {
            PrintPhase("ДИНАМИЧЕСКОЕ СОЗДАНИЕ ПЕРСОНАЖЕЙ");

            var random = new Random();
            var factoryTypes = new Dictionary<int, Func<CharacterFactory>>
            {
                { 1, () => new MageFactory() },
                { 2, () => new WarriorFactory() },
                { 3, () => new ArcherFactory() }
            };

            Console.WriteLine("Генерация случайной команды:\n");
            var randomTeam = new List<ICharacter>();
            string[] randomNames = { "Артур", "Эльза", "Дрейк", "Сильвана", "Гром" };

            for (int i = 0; i < 5; i++)
            {
                int classChoice = random.Next(1, 4);
                int level = random.Next(1, 11);
                string name = randomNames[i];

                var factory = factoryTypes[classChoice]();
                var character = factory.CreateCharacter(name, level);
                randomTeam.Add(character);

                Console.WriteLine($"Создан [{factory.GetCharacterType()}]: {character.GetInfo()}");
                System.Threading.Thread.Sleep(300);
            }

            Console.WriteLine($"\n Всего создано персонажей: {randomTeam.Count}");
            Console.WriteLine($" Классов: 3 (Маг, Воин, Лучник)");
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {title,-58} ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════╝");
        }

        static void PrintPhase(string phase)
        {
            Console.WriteLine($"\n┌────────────────────────────────────────────────────────┐");
            Console.WriteLine($" {phase,-52} │");
            Console.WriteLine($"└────────────────────────────────────────────────────────┘");
        }

        static void PrintDescription()
        {
            Console.WriteLine("\n ОПИСАНИЕ ПАТТЕРНА:");
            Console.WriteLine("• Абстрактный продукт: ICharacter");
            Console.WriteLine("• Конкретные продукты: Mage, Warrior, Archer");
            Console.WriteLine("• Абстрактная фабрика: CharacterFactory");
            Console.WriteLine("• Конкретные фабрики: MageFactory, WarriorFactory, ArcherFactory");
            Console.WriteLine("\n Преимущества:");
            Console.WriteLine("  - Код не зависит от конкретных классов персонажей");
            Console.WriteLine("  - Легко добавлять новых персонажей");
            Console.WriteLine("  - Централизованное создание объектов");
        }
    }
}