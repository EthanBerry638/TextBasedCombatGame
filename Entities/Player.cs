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
        public List<Potion> Potions = new List<Potion> { };
        public bool IsEmpty { get; set; }

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

        public bool IsInventoryEmpty()
        {
            if (Potions.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void ViewInventory()
        {
            Console.WriteLine("Potions: \n");
            Helper.Pause(200);
            if (Potions.Count == 0)
            {
                Console.WriteLine("There are no potions in the inventory.");
            }
            else
            {
                for (int i = 0; i < Potions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Potions[i].Name}");
                }
            }
            Console.WriteLine("Please press Enter to return to main menu...");
            Console.ReadLine();
        }

        public void UsePotion()
        {
            while (true)
            {
                ViewInventory();

                if (Potions.Count == 0)
                {
                    return;
                }

                Console.WriteLine("Enter the number of the potion to use: ");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || choice < 1 || choice > Potions.Count)
                {
                    Console.WriteLine("That's not a valid choice. Try again.\n");
                    continue;
                }

                var selectedPotion = Potions[choice - 1];
                // TODO: make applyeffect function selectedPotion.ApplyEffect();
                Potions.RemoveAt(choice - 1);

                Console.WriteLine($"You used {selectedPotion.Name}!");
                Helper.Pause(500);
                return;
            }
        }
    }
}