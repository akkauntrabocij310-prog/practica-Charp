using System;
using System.Collections.Generic;
using WeaponDecorator.Models;
using WeaponDecorator.Models.Decorators;

namespace WeaponDecorator
{
    class Program
    {
        static Random random = new Random();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Оружие - Паттерн Декоратор";

            PrintHeader("ПАТТЕРН ДЕКОРАТОР");
            PrintDescription();

            // 1. Базовое оружие
            DemonstrateBasicWeapon();

            // 2. Одиночные улучшения
            DemonstrateSingleEnhancements();

            // 3. Комбинации улучшений
            DemonstrateCombinedEnhancements();

            // 4. Сложная комбинация всех улучшений
            DemonstrateFullUpgrade();

            // 5. Сравнение характеристик
            DemonstrateComparison();

            Console.WriteLine("\n\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void DemonstrateBasicWeapon()
        {
            PrintPhase("1. БАЗОВОЕ ОРУЖИЕ (БЕЗ УЛУЧШЕНИЙ)");

            IWeapon sword = new BasicWeapon("Стальной меч", 25, 300, "Обычное");
            DisplayWeaponInfo(sword);
            SimulateAttack(sword);
        }

        static void DemonstrateSingleEnhancements()
        {
            PrintPhase("2. ОРУЖИЕ С ОДИНОЧНЫМИ УЛУЧШЕНИЯМИ");

            IWeapon basicSword = new BasicWeapon("Длинный меч", 30, 400, "Обычное");

            // Огненное улучшение
            IWeapon fireSword = new FireEnhancementDecorator(basicSword, 20, 35);
            DisplayWeaponInfo(fireSword, "Огненный меч");
            SimulateAttack(fireSword);

            // Ледяное улучшение
            IWeapon iceSword = new IceEnhancementDecorator(basicSword, 18, 45);
            DisplayWeaponInfo(iceSword, "Ледяной меч");
            SimulateAttack(iceSword);

            // Критическое улучшение
            IWeapon critSword = new CriticalHitDecorator(basicSword, 30, 2.2);
            DisplayWeaponInfo(critSword, "Меч критического удара");
            SimulateAttack(critSword);
        }

        static void DemonstrateCombinedEnhancements()
        {
            PrintPhase("3. КОМБИНАЦИИ УЛУЧШЕНИЙ");

            IWeapon basicAxe = new BasicWeapon("Боевой топор", 35, 450, "Необычное");

            // Огонь + Лед (противоположные стихии)
            IWeapon fireAxe = new FireEnhancementDecorator(basicAxe, 18, 30);
            IWeapon fireIceAxe = new IceEnhancementDecorator(fireAxe, 15, 40);
            DisplayWeaponInfo(fireIceAxe, "Магический топор (Огонь + Лёд)");
            SimulateAttack(fireIceAxe);

            // Огонь + Крит
            IWeapon fireCritAxe = new CriticalHitDecorator(
                new FireEnhancementDecorator(basicAxe, 22, 35),
                35, 2.5);
            DisplayWeaponInfo(fireCritAxe, "Топор адского пламени");
            SimulateAttack(fireCritAxe);
        }

        static void DemonstrateFullUpgrade()
        {
            PrintPhase("4. МАКСИМАЛЬНОЕ УЛУЧШЕНИЕ (ВСЕ ДЕКОРАТОРЫ)");

            IWeapon legendaryWeapon = new BasicWeapon("Древний клинок", 40, 800, "Редкое");

            // Наслаиваем все улучшения
            legendaryWeapon = new FireEnhancementDecorator(legendaryWeapon, 25, 40);
            legendaryWeapon = new IceEnhancementDecorator(legendaryWeapon, 20, 45);
            legendaryWeapon = new CriticalHitDecorator(legendaryWeapon, 40, 2.8);

            DisplayWeaponInfo(legendaryWeapon, " ЛЕГЕНДАРНЫЙ КЛИНОК ДРЕВНИХ ");

            // Несколько атак для демонстрации критов
            Console.WriteLine("\n  Испытание оружия (3 атаки):");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"  Атака {i + 1}: ");
                SimulateAttack(legendaryWeapon, false);
                System.Threading.Thread.Sleep(500);
            }
        }

        static void DemonstrateComparison()
        {
            PrintPhase("5. СРАВНЕНИЕ ХАРАКТЕРИСТИК");

            var weapons = new List<(IWeapon weapon, string name)>
            {
                (new BasicWeapon("Обычный меч", 25, 300, "Обычное"), "Базовый меч"),
                (new FireEnhancementDecorator(new BasicWeapon("Меч", 25, 300, "Обычное"), 20, 35), "Огненный меч"),
                (new IceEnhancementDecorator(new BasicWeapon("Меч", 25, 300, "Обычное"), 18, 40), "Ледяной меч"),
                (new CriticalHitDecorator(new BasicWeapon("Меч", 25, 300, "Обычное"), 30, 2.2), "Меч крита"),
                (new FireEnhancementDecorator(
                    new IceEnhancementDecorator(
                        new CriticalHitDecorator(
                            new BasicWeapon("Меч", 25, 300, "Обычное"), 35, 2.5), 20, 40), 25, 35), "Полный апгрейд")
            };

            Console.WriteLine("\n  Сравнительная таблица:");
            Console.WriteLine("  ┌──────────────┬────────────┬────────────┬──────────────────┐");
            Console.WriteLine("  │ Оружие       │ Урон       │ Стоимость  │ Редкость         │");
            Console.WriteLine("  ├──────────────┼────────────┼────────────┼──────────────────┤");

            foreach (var (weapon, name) in weapons)
            {
                Console.WriteLine($"  │ {name,-12} │ {weapon.GetDamage(),-10} │ {weapon.GetCost(),-10} │ {weapon.GetRarity(),-16} │");
            }

            Console.WriteLine("  └──────────────┴────────────┴────────────┴──────────────────┘");

            // Расчет эффективности
            Console.WriteLine("\n   Анализ эффективности (урон/стоимость):");
            foreach (var (weapon, name) in weapons)
            {
                double efficiency = (double)weapon.GetDamage() / weapon.GetCost();
                Console.WriteLine($"  • {name}: {efficiency:F3} урона за 1 золотой");
            }
        }

        static void DisplayWeaponInfo(IWeapon weapon, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
                Console.WriteLine($"\n   {title}:");

            Console.WriteLine($"   Описание: {weapon.GetDescription()}");
            Console.WriteLine($"   Урон: {weapon.GetDamage()}");
            Console.WriteLine($"   Стоимость: {weapon.GetCost()} золотых");
            Console.WriteLine($"  Редкость: {weapon.GetRarity()}");
        }

        static void SimulateAttack(IWeapon weapon, bool showDetails = true)
        {
            int damage = weapon.GetDamage();

            if (showDetails)
                Console.WriteLine($"   Атака оружием: {weapon.GetDescription().Split('+')[0].Trim()}");

            // Проверяем наличие декораторов и применяем их эффекты
            var currentWeapon = weapon;
            bool hasFire = false, hasIce = false, hasCrit = false;
            CriticalHitDecorator critDecorator = null;

            while (currentWeapon is WeaponDecorator decorator)
            {
                if (decorator is FireEnhancementDecorator fire)
                {
                    hasFire = true;
                    if (showDetails) fire.ApplyFireEffect();
                    damage = fire.GetDamage();
                }
                if (decorator is IceEnhancementDecorator ice)
                {
                    hasIce = true;
                    if (showDetails) ice.ApplyIceEffect();
                    damage = ice.GetDamage();
                }
                if (decorator is CriticalHitDecorator crit)
                {
                    hasCrit = true;
                    critDecorator = crit;
                }
                currentWeapon = decorator._weapon;
            }

            // Применяем критический удар (если есть)
            if (hasCrit && critDecorator != null)
            {
                int originalDamage = damage;
                damage = critDecorator.CalculateCriticalDamage(originalDamage, random);
            }
            else
            {
                if (showDetails)
                    Console.WriteLine($"   Нанесено урона: {damage}");
            }

            if (!showDetails)
                Console.WriteLine($"Нанесено {damage} урона");

            Console.WriteLine();
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
            Console.WriteLine($"│   {phase,-52} │");
            Console.WriteLine($"└────────────────────────────────────────────────────────┘");
        }

        static void PrintDescription()
        {
            Console.WriteLine("\n ОПИСАНИЕ ПАТТЕРНА:");
            Console.WriteLine("• Компонент: IWeapon");
            Console.WriteLine("• Конкретный компонент: BasicWeapon");
            Console.WriteLine("• Абстрактный декоратор: WeaponDecorator");
            Console.WriteLine("• Конкретные декораторы: FireEnhancement, IceEnhancement, CriticalHit");
            Console.WriteLine("\n Преимущества:");
            Console.WriteLine("  - Динамическое добавление функциональности");
            Console.WriteLine("  - Гибкое комбинирование улучшений");
            Console.WriteLine("  - Сохранение интерфейса");
            Console.WriteLine("  - Замена наследования");
        }
    }
}