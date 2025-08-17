using System;
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

        public Player(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
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
    }
}