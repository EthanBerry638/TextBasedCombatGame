using System;

namespace TextBasedCombat.Entities
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; private set; }
        public int AttackPower { get; set; }
        public int CritChance { get; set; } = 20;
        public double CritMultiplier { get; set; } = 2.0;

        public Enemy(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        public void Attack(Player player, Random random)
        {
            bool isCrit = random.Next(0, 100) < CritChance; // 20% chance

            int damage = isCrit ? (int)(AttackPower * CritMultiplier) : AttackPower;

            Console.WriteLine($"{Name} attacks {player.Name} for {damage} damage{(isCrit ? " (CRITICAL HIT!)" : "")}!");
            Utils.Pause(500);
            player.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} takes {damage} damage. Remaining health: {Health}");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}