using TextBasedCombat.Entities;

namespace TextBasedCombat.Combat
{
    public static class CombatManager
    {
        public static void StartCombat(Player player, Enemy enemy, Random random, ref bool exitMain)
        {
            bool playerWon = CombatLoop.Run(player, enemy, random);

            if (!playerWon)
            {
                Console.WriteLine("Game over...\nPress enter to exit...");
                exitMain = true;
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Press enter to return to main menu!");
                Console.ReadLine();
            }
        }
    }
}