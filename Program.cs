﻿using System;
using System.Threading;
using TextBasedCombat.CharacterCreation;
using TextBasedCombat.Entities;
using TextBasedCombat.MenuManager;
using TextBasedCombat.Utils;
using TextBasedCombat.Combat;

Random sharedRandom = new Random();
CharacterCreator creator = new CharacterCreator(sharedRandom);
Player player = creator.CharacterCreationMenu();
bool firstFight = creator.firstFight; 