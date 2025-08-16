using TextBasedCombat.Entities;

namespace TextBasedCombat.Combat
{
    public static class CombatLoop
    {
        public static bool Run(Player player, Enemy enemy, Random random)
        {
            while (player.IsAlive() && enemy.IsAlive())
            {
                player.Attack(enemy, random);
                if (enemy.IsAlive())
                {
                    enemy.Attack(player, random);
                }
            }

            Console.WriteLine(player.IsAlive() ? "\nYou win!\n" : "\nYou were defeated...\n");
            return player.IsAlive(); // true if player won, false if defeated
        }
    }
}