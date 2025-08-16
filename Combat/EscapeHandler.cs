using TextBasedCombat.Entities;
using TextBasedCombat.Utils;

namespace TextBasedCombat.Combat
{
    public static class EscapeHandler
    {
        public static void FleeCombat(Player player, Enemy enemy, Random random)
        {
            Console.WriteLine($"{player.Name} attempts to flee from the {enemy.Name}...");
            Helper.Pause(500);
            int escapeChance = random.Next(0, 11);
            if (escapeChance >= 5)
            {
                Console.WriteLine("You succeeded!");
                Console.WriteLine("That's a relief! Press enter to return to main menu...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Uh oh... {enemy.Name} caught you");
                Console.WriteLine("You died at their hands...\nGame over\nPress enter to exit...");
                Console.ReadLine();
            }
        }
    }
}