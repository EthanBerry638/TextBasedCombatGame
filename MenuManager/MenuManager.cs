// Menu loop and options are contained here

using TextBasedCombat.CharacterCreation;
using TextBasedCombat.Entities;
using TextBasedCombat.Utils;
using TextBasedCombat.Combat;
using TextBasedCombat.GlobalFlags;

namespace TextBasedCombat.MenuManager
{
    public class MenuManager
    {
        private Player player;
        private Random sharedRandom;
        private CharacterCreator creator;
        private Potion potion;
        string? readInput { get; set; }
        bool hasPlayed { get; set; }
        private bool firstFight;

        public MenuManager(Player player, Random sharedRandom)
        {
            this.player = player;
            this.sharedRandom = sharedRandom;
            this.creator = new CharacterCreator(sharedRandom);
        }

        public void MainMenu()
        {
            while (!Flags.exitMain)
                {
                    DisplayMainMenu();

                    bool validInput = false;
                    while (!validInput)
                    {
                        int choice = GetMainMenuChoice();
                        if (choice != -1)
                        {
                            validInput = HandleMainMenuChoice(choice);
                        }
                    }
                }
        }
        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu\n");
            Console.WriteLine($"{(hasPlayed ? Helper.StrikeThrough("1. New game") : "1. New game")}");
            Console.WriteLine($"{(!hasPlayed ? Helper.StrikeThrough("2. Continue game") : "2. Continue game")}");
            Console.WriteLine($"{(!hasPlayed ? Helper.StrikeThrough("3. View character stats") : "3. View character stats")}");
            Console.WriteLine($"{(!hasPlayed || player.IsInventoryEmpty() ? Helper.StrikeThrough("4. View character inventory") : "4. View character inventory")}"); 
            Console.WriteLine("5. Tutorial\n6. Credits/Lore\n7. Exit");
            Helper.Pause(2000);
            Console.WriteLine("\nPlease select a number from the list above!");
        }
        private int GetMainMenuChoice()
        {
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter something.");
                return -1;
            }

            if (int.TryParse(input, out int choice))
            {
                return choice;
            }

            Console.WriteLine("Invalid input. Please enter a number.");
            return -1;
        }
        bool HandleMainMenuChoice(int choice)
        {
            switch (choice) // TODO: Refactor using enums. Magic numbers
            {
                case 1: // New game
                    if (hasPlayed)
                    {
                        Console.WriteLine("You already have a save file...\nPlease select continue.");
                        Helper.Pause(500);
                        return false;
                    }
                    hasPlayed = true;
                    player = creator.CharacterCreationMenu();
                    firstFight = creator.firstFight;
                    EncounterSetup.SetupEncounter(player, creator, sharedRandom, ref firstFight);
                    return true;
                case 2: // Continue
                    if (!hasPlayed)
                    {
                        Console.WriteLine("You don't currently have a save file...\nPlease select new game.");
                        Helper.Pause(500);
                        return false;
                    }
                    EncounterSetup.SetupEncounter(player, creator, sharedRandom, ref firstFight);
                    return true;
                case 3: // View character stats
                    try
                    {
                        ViewCharacterStats();
                        return true;
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Null reference exception. Player has not been set yet. Return to main by pressing enter...");
                        Console.ReadLine();
                        return true;
                    }
                case 4: // View character inventory
                    try
                    {
                        player.ViewInventory();
                        return true;
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Null reference exception. Nothing in inventory. Return to main by pressing enter...");
                        Console.ReadLine();
                        return true;
                    }
                case 5: // Tutorial
                    Tutorial();
                    return true;
                case 6: // CreditsAndLore
                    CreditsAndLore();
                    return true;
                case 7: // Exit main menu
                    Flags.exitMain = true;
                    Console.WriteLine("Thank you for playing my turn based RPG game!");
                    return true;
                default:
                    Console.WriteLine("Please enter a valid number from the list.");
                    return false;
            }
        }

        void ViewCharacterStats()
        {
            Console.Clear();
            Console.WriteLine($"Your character {player.Name} current stats are: ");
            Console.WriteLine($"Health: {player.Health}\nAttack Power: {player.AttackPower}\nTheir level is {player.Level}\n");
            Console.WriteLine("Press any key to return to main...");
            Console.ReadLine();
        }
        private void Tutorial()
        {
            Console.Clear();
            Console.WriteLine("This turn based fighting game consists of 1 player (YOU!) and 1 enemy...");
            Helper.Pause(1000);
            Console.WriteLine("You can create a custom character or be assigned a random default character...");
            Helper.Pause(1000);
            Console.WriteLine("Then you will be thrown into combat with a random enemy...");
            Helper.Pause(500);
            Console.WriteLine("From there you can choose to either flee or fight the enemy...");
            Helper.Pause(500);
            Console.WriteLine("If you choose to fight the enemy combat will begin. At the current stage of development the player and enemy will automatically attack each other. Each have a chance to critically hit. When the player's health hits 0, the game ends...");
            Helper.Pause(10000);
            Console.WriteLine("If you choose to flee, you have a 50/50 chance of escaping...");
            Helper.Pause(500);
            Console.WriteLine("If you fail to flee, your game will be over, otherwise you will return to the main menu where you can decide what to do next...");
            Helper.Pause(5000);
            Console.WriteLine("And that's about all so far! Whenever you're ready, press any key to return to the main menu...");
            Console.ReadLine();
        }

        private void CreditsAndLore()
        {
            Console.Clear();
            Console.WriteLine("=== Credits ===\n");
            Console.WriteLine("Game Design & Programming: Ethan");
            Console.WriteLine("Story & Lore: Ethan");

            Console.WriteLine("=== Lore ===\n");
            Console.WriteLine("In the fractured realm of Eldros, the balance between light and shadow has crumbled.");
            Console.WriteLine("Ancient beasts stir beneath the soil, and whispers of forgotten magic echo through the ruins.");
            Console.WriteLine("You are a lone adventurer—chosen not by prophecy, but by circumstance.");
            Console.WriteLine("Whether fighter, mage, ranger, or bruiser, your path is forged in blood and fire.");
            Console.WriteLine("Survive the encounters. Uncover the truth. And maybe, just maybe, restore what was lost.");
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadKey();
        }
    }
}