using TextBasedCombat.Entities;
using TextBasedCombat.GlobalFlags;

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
            Console.ReadKey(true);
            if (!player.IsAlive())
            {
                Flags.exitMain = true;
            }
            else
            {
                player.GainXP(player);
                if (player.IsLevellingUp(player))
                {
                    player.LevelUp(player);
                    Console.WriteLine($"{player.Name} levlelled up! Your level is now {player.Level}");
                }
            }
            return player.IsAlive(); // true if player won, false if defeated
        }
    }
}