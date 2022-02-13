using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace EasyCustomStuff
{
	public class ExampleCompanion : CustomCompanion
	{
		protected override void Setup()
		{
			ModifyExistingCompanion("Anton");
			Name = "Dummy";
			UsesSlap = false;
			UsesAllAbilities = true;
			UsesOverworldMovingAnim = false;

			CreateSprite("Overworld", "EasyCustomStuff/assets/dummyoverworld.png");
			CreateSprite("Battle front", "EasyCustomStuff/assets/dummy.png");
			CreateSprite("Battle back", "EasyCustomStuff/assets/dummy.png");

			SetSprite(ref Companion.characterOWSprite, "Overworld");
			SetSprite(ref Companion.characterSprite, "Battle front");
			SetSprite(ref Companion.characterBackSprite, "Battle back");

			/*
			// 1
			SelectableCharactersSO foo = Traverse.Create("MainMenuController").Field("_charSelectionDB").GetValue<SelectableCharactersSO>();
			// 2
			SelectableCharacterData[] array = foo.Characters;
			// 3
			var x = GetAllLoadedCompanions();
			x.Add(Name, Companion);
			// 4
			Traverse.Create(array[0]).Field("_characterName").SetValue(Name);
			// 5
			GameInformationHolder y = Traverse.Create("MainMenuController").Field("_informationHolder").GetValue<GameInformationHolder>();
			y.Game.UnlockedCharacters.Add(Name);
			*/

		} // end Setup

	} // end class ExampleCompanion

} // end namespace EasyCustomStuff