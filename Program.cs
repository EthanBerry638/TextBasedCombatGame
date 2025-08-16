﻿using System;
using System.Threading;
using TextBasedCombat.CharacterCreation;
using TextBasedCombat.Entities;

Player player = null!;
Random random = new Random();
CharacterCreation creator = new CharacterCreation(random);

string? readInput = "";

bool hasPlayed = false;
bool firstFight = false;
bool exitMain = false;

MainMenu();

void MainMenu()
{
    while (!exitMain)
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

void DisplayMainMenu()
{
    Console.Clear();
    Console.WriteLine("Main Menu\n");
    Console.WriteLine($"{(hasPlayed ? StrikeThrough("1. New game") : "1. New game")}");
    Console.WriteLine($"{(!hasPlayed ? StrikeThrough("2. Continue game") : "2. Continue game")}");
    Console.WriteLine($"{(!hasPlayed ? StrikeThrough("3. View character stats") : "3. View character stats")}");
    Console.WriteLine("4. Tutorial\n5. Credits/Lore\n6. Exit");
    Utils.Pause(2000);
    Console.WriteLine("\nPlease select a number from the list above!");
}

int GetMainMenuChoice()
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
    switch (choice)
    {
        case 1:
            if (hasPlayed)
            {
                Console.WriteLine("You already have a save file...\nPlease select continue.");
                Utils.Pause(500);
                return false;
            }
            hasPlayed = true;
            player = creator.CharacterCreationMenu();
            firstFight = creator.firstFight;
            SetupEncounter();
            return true;

        case 2:
            if (!hasPlayed)
            {
                Console.WriteLine("You don't currently have a save file...\nPlease select new game.");
                Utils.Pause(500);
                return false;
            }
            SetupEncounter();
            return true;

        case 3:
            ViewCharacterStats();
            return true;

        case 4:
            Tutorial();
            return true;

        case 5:
            CreditsAndLore();
            return true;

        case 6:
            exitMain = true;
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
    Console.WriteLine($"Health: {player.Health}\nAttack Power: {player.AttackPower}");
    Console.WriteLine("Press any key to return to main...");
    Console.ReadLine();
}

void Tutorial()
{
    Console.Clear();
    Console.WriteLine("This turn based fighting game consists of 1 player (YOU!) and 1 enemy...");
    Utils.Pause(1000);
    Console.WriteLine("You can create a custom character or be assigned a random default character...");
    Utils.Pause(1000);
    Console.WriteLine("Then you will be thrown into combat with a random enemy...");
    Utils.Pause(500);
    Console.WriteLine("From there you can choose to either flee or fight the enemy...");
    Utils.Pause(500);
    Console.WriteLine("If you choose to fight the enemy combat will begin. At the current stage of development the player and enemy will automatically attack each other. Each have a chance to critically hit. When the player's health hits 0, the game ends...");
    Utils.Pause(10000);
    Console.WriteLine("If you choose to flee, you have a 50/50 chance of escaping...");
    Utils.Pause(500);
    Console.WriteLine("If you fail to flee, your game will be over, otherwise you will return to the main menu where you can decide what to do next...");
    Utils.Pause(5000);
    Console.WriteLine("And that's about all so far! Whenever you're ready, press any key to return to the main menu...");
    Console.ReadLine();
}

void CreditsAndLore()
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

string StrikeThrough(string text)
{
    return string.Concat(text.Select(c => $"{c}\u0336"));
}

void FleeCombat(Enemy enemy)
{
    Console.WriteLine($"{player.Name} attempts to flee from the {enemy.Name}...");
    Utils.Pause(500);
    int escapeChance = random.Next(0, 11);
    if (escapeChance >= 5)
    {
        Console.WriteLine("You succeeded!");
        Console.WriteLine("That's a relief! Press enter to return to main menu...");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine($"Uh oh... {enemy.Name} caught you");
        Console.WriteLine("You died at their hands...\nGame over\nPress enter to exit...");
        Console.ReadLine();
    }
}

void CombatLoop(Enemy enemy)
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
    if (!player.IsAlive())
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

void SetupEncounter()
{
    Enemy enemy = creator.CreateEnemy();
    Console.WriteLine($"Oh no! An enemy {enemy.Name} has been encountered!");
    Console.WriteLine($"They have {enemy.Health} health and {enemy.AttackPower} attack power!\n");
    Utils.Pause(1000);
    if (!firstFight)
    {
        Console.WriteLine($"Do you choose to flee or fight? Your current health is {player.Health}...");
        Utils.Pause(1000);
        bool correctInput = false;
        while (!correctInput)
        {
            readInput = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (string.IsNullOrWhiteSpace(readInput))
            {
                Console.WriteLine("Please enter something.");
            }
            else if (readInput != "fight" && readInput != "flee")
            {
                Console.WriteLine("Please enter 'fight' or 'flee'");
            }
            else
            {
                correctInput = true;
            }
        }
        if (readInput == "fight")
        {
            CombatLoop(enemy);
        }
        else if (readInput == "flee")
        {
            FleeCombat(enemy);
        }
    }
    else
    {
        CombatLoop(enemy);
        firstFight = false;
    }
}

public static class Utils
{
    public static void Pause(int ms) => Thread.Sleep(ms);
}