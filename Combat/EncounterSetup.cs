using TextBasedCombat.CharacterCreation;
using TextBasedCombat.Entities;
using TextBasedCombat.Utils;

namespace TextBasedCombat.Combat
{
    public static class EncounterSetup
    {
        public static void SetupEncounter(Player player, CharacterCreator creator, Random random, ref bool firstFight)
        {
            Enemy enemy = creator.CreateEnemy();
            Console.WriteLine($"Oh no! An enemy {enemy.Name} has been encountered!");
            Console.WriteLine($"They have {enemy.Health} health and {enemy.AttackPower} attack power!\n");
            Helper.Pause(1000);
            if (!firstFight)
            {
                Console.WriteLine($"Do you choose to flee or fight? Your current health is {player.Health}...");
                Helper.Pause(1000);
                string readInput = "";
                bool correctInput = false;
                while (!correctInput)
                {
                    readInput = Console.ReadLine()?.Trim().ToLower() ?? "";
                    if (string.IsNullOrWhiteSpace(readInput))
                    {
                        Console.WriteLine("Please enter something.");
                    }
                    else if (readInput != "fight" && readInput != "flee")
                    {
                        Console.WriteLine("Please enter 'fight' or 'flee'");
                    }
                    else
                    {
                        correctInput = true;
                    }
                }
                if (readInput == "fight")
                {
                    CombatLoop.Run(player, enemy, random);
                }
                else if (readInput == "flee")
                {
                    EscapeHandler.FleeCombat(player, enemy, random);
                }
            }
            else
            {
                CombatLoop.Run(player, enemy, random);
                firstFight = false;
            }
        }
    }
}