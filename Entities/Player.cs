using System;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using TextBasedCombat.Utils;

namespace TextBasedCombat.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; private set; }
        public int AttackPower { get; set; }
        public int CritChance { get; set; } = 20;
        public double CritMultiplier { get; set; } = 2.0;
        public int Level { get; set; }
        public int XP = 0;

        public Player(string name, int health, int attackPower, int level)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Level = level;
        }

        public void Attack(Enemy enemy, Random random)
        {
            bool isCrit = random.Next(0, 100) < CritChance; // 20% chance

            int damage = isCrit ? (int)(AttackPower * CritMultiplier) : AttackPower;

            Console.WriteLine($"{Name} attacks {enemy.Name} for {damage} damage{(isCrit ? " (CRITICAL HIT!)" : "")}!");
            Helper.Pause(500);
            enemy.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} takes {damage} damage. Remaining health: {Health}!");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public bool IsLevellingUp(Player player)
        {
            int levelUpThreshold = CalculateLevelUpThreshold(player.Level);

            if (player.XP >= levelUpThreshold)
            {
                player.LevelUp(player); 
                return true;
            }

            return false;
        }

        private int CalculateLevelUpThreshold(int level)
        {
            return level * 2;
        }

        public int GainXP(Player player)
        {
            return player.XP += 1;
        }

        public int LevelUp(Player player)
        {
            player.XP = 0;
            return player.Level += 1;
        }
    }
}