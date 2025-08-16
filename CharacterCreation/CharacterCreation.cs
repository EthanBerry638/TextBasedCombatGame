using TextBasedCombat.Entities;
using TextBasedCombat.Utils;

// Character creation menus and creation functions

namespace TextBasedCombat.CharacterCreation
{
    public class CharacterCreator
    {
        public string? readInput { get; private set; } 
        public bool firstFight { get; private set; }
        private readonly Random random;

        public CharacterCreator(Random sharedRandom)
        {
            random = sharedRandom;
        }

        public Player CharacterCreationMenu()
        {
            bool input = false;
            Player player = null!;
            while (!input)
            {
                Console.WriteLine("\nWould you like to use a random, default character? Or create your own?\n\n");
                Helper.Pause(500);
                Console.WriteLine("1. Default\n2. Custom");
                readInput = Console.ReadLine();
                if (readInput != null)
                {
                    readInput = readInput.Trim();
                }

                int.TryParse(readInput, out var choice);

                switch (choice)
                {
                    case 1:
                        player = CreateDefault();
                        input = true;
                        break;
                    case 2:
                        player = CreateCustom();
                        input = true;
                        break;
                    default:
                        Console.WriteLine("Please enter 1 or 2.");
                        break;
                }
            }

            return player!;
        }
        private Player CreateCustom()
        {
            firstFight = true;
            string? nameChoice;
            int health = 0;
            int attackPower = 0;

            Console.WriteLine("Please enter the name for your character: ");
            Helper.Pause(500);
            nameChoice = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nameChoice))
            {
                Console.WriteLine("Invalid name. Using 'Adventurer' as default.");
                nameChoice = "Adventurer";
            }

            Console.WriteLine($"You have chosen {nameChoice} as your name!");

            CharacterClass classChoice = 0;
            bool validClass = false;

            while (!validClass)
            {
                Console.WriteLine("What class would you like to make your character? Please enter a number from the list below...");
                Helper.Pause(1000);
                PrintClasses();

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice) &&
                    Enum.IsDefined(typeof(CharacterClass), choice))
                {
                    classChoice = (CharacterClass)choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 4.");
                }

                switch (classChoice)
                {
                    case CharacterClass.Fighter:
                        health = 100;
                        attackPower = 15;
                        validClass = true;
                        break;
                    case CharacterClass.Mage:
                        health = 40;
                        attackPower = 30;
                        validClass = true;
                        break;
                    case CharacterClass.Ranger:
                        health = 50;
                        attackPower = 25;
                        validClass = true;
                        break;
                    case CharacterClass.Bruiser:
                        health = 75;
                        attackPower = 20;
                        validClass = true;
                        break;
                    default:
                        // Should never hit this due to earlier input validation.
                        break;
                }
            }

            Console.WriteLine($"You have chosen class {classChoice} with {health} health and {attackPower} attack power!\n");

            return new Player(nameChoice, health, attackPower);
        }

        private Player CreateDefault()
        {
            firstFight = true;
            List<Player> defaultCharacters = new List<Player>
            {
                new Player("Darius", 100, 15),
                new Player("Gale", 80, 25),
                new Player("Lora", 120, 10),
                new Player("Lyra", 90, 20)
            };

            int listCount = defaultCharacters.Count;
            int chosen = random.Next(0, listCount);

            Console.WriteLine("\nYour default character has been created!\n");
            Helper.Pause(500);
            Console.WriteLine($"Their name is {defaultCharacters[chosen].Name}! They have {defaultCharacters[chosen].Health} health and {defaultCharacters[chosen].AttackPower} attack power!\n");
            Helper.Pause(1000);

            return defaultCharacters[chosen];
        }
        public Enemy CreateEnemy()
        {
            List<Enemy> defaultEnemies = new List<Enemy>
            {
                new Enemy("Zombie", 200, 5),
                new Enemy("Dragon", 500, 2),
                new Enemy("Witch", 60, 40),
                new Enemy("Goblin", 75, 25)
            };

            int listCount = defaultEnemies.Count;
            int chosen = random.Next(0, listCount);

            return defaultEnemies[chosen];
        }
        private void PrintClasses()
        {
            Console.WriteLine("The current classes are as follows: ");
            Console.WriteLine("1. Fighter\n2. Mage\n3. Ranger\n4. Bruiser");
        }
    }
}