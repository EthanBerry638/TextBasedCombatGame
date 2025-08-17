﻿using System;
using System.Threading;
using TextBasedCombat.CharacterCreation;
using TextBasedCombat.Entities;
using TextBasedCombat.MenuManager;

Random sharedRandom = new Random();
Player player = null!;

var menuManager = new MenuManager(player, sharedRandom);
menuManager.MainMenu();