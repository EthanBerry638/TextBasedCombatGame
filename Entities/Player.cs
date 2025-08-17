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
        public int Level { get; set; } = 1;
        public int XP { get; set; } = 0;
        public List<string> Potions = new List<string> { };

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

        public bool IsLevellingUp()
        {
            int levelUpThreshold = CalculateLevelUpThreshold(this.Level);

            if (this.XP >= levelUpThreshold)
            {
                this.LevelUp();
                return true;
            }

            return false;
        }

        private int CalculateLevelUpThreshold(int level)
        {
            return level * 2;
        }

        public void GainXP(int amount)
        {
            XP += amount;
            int threshold = CalculateLevelUpThreshold(Level);
            if (XP >= threshold)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            int healthModifier;
            int attackPowerModifier;
            XP = 0;
            Level += 1;
            attackPowerModifier = Level * 3;
            healthModifier = Level * 3;
            Health += healthModifier;
            AttackPower += attackPowerModifier;
            Console.WriteLine($"{Name} levelled up! Your level is now {Level}.\nYour health increased by {healthModifier} and your attack power increased by {attackPowerModifier}.\nYour health is now {Health} and your attack power is {AttackPower}.");
        }

        public void AddPotions()
        {
            
        }
    }
}