﻿using System;
using System.Threading;
using TextBasedCombat.CharacterCreation;
using TextBasedCombat.Entities;
using TextBasedCombat.MenuManager;

Random sharedRandom = new Random();
CharacterCreator creator = new CharacterCreator(sharedRandom);
Player player = creator.CharacterCreationMenu();

var menuManager = new MenuManager(player, sharedRandom);
menuManager.MainMenu();