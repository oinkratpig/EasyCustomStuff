using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx;
using HarmonyLib;

namespace EasyCustomStuff
{
	abstract class CustomCompanion
	{
		private static int nextEntityID = 100000;
		private static List<CustomCompanion> allCustomCompanions = new();
		private CharacterSO _companionSO;
		private EntityIDs _companionEntityID;
		private bool _isModifyingCompanion;

		/* ScriptableObject instance holding this companion's data */
		protected CharacterSO Companion
		{
			get
			{
				return _companionSO;
			}
			set
			{
				_companionSO = value;
			}

		} // end property Companion

		/* Name of this companion */
		public string Name
		{
			get
			{
				return Companion.characterName;
			}
			set
			{
				Companion.characterName = value;
			}

		} // end property Name

		/* EntityID of this companion */
		public EntityIDs EntityID
		{
			get
			{
				return Companion.characterEntityID;
			}
			private set
			{
				Companion.characterEntityID = value;
			}

		} // end property EntityID

		/* Whether or not this companion always has their basic (slap) ability */
		public bool UsesSlap
		{
			get
			{
				return Companion.usesBasicAbility;
			}
			set
			{
				Companion.usesBasicAbility = value;
			}

		} // end property UsesSlap

		/* Does this companion have access to ALL of their abilities at once?
		 * (Slap does not count as an ability - see UsesSlap) */
		public bool UsesAllAbilities
		{
			get
			{
				return Companion.usesAllAbilities;
			}
			set
			{
				Companion.usesAllAbilities = value;
			}

		} // end property UsesAllAbilities

		/* Whether or not to use the walking animation in the overworld */
		public bool UsesOverworldMovingAnim
		{
			get
			{
				return Companion.movesOnOverworld;
			}
			set
			{
				Companion.movesOnOverworld = value;
			}

		} // end property UsesOverworldMovingAnim

		/* Ran directly after a custom companion is initialized.
		 * Used as a constructor sort-of. */
		public abstract void Setup();

		/* Constructor */
		public CustomCompanion()
		{
			allCustomCompanions.Add(this);

		} // end CustomCompanion

		/* Prepares the custom companion for use */
		private void Initialize()
		{
			_companionEntityID = (EntityIDs) nextEntityID++;
			LoadExistingCompanion("Anton");

		} // end Initialize

		/* On the main menu, create all of the custom companions */
		[HarmonyPatch(typeof(MainMenuController), "Start")]
		class PatchMainMenuController
		{
			[HarmonyPostfix]
			private static void Postfix(MainMenuController __instance)
			{
				Plugin.Log.LogInfo("Loading custom companions...");
				if (allCustomCompanions.Count == 0)
				{
					Plugin.Log.LogInfo("No companions to load.");
					return;
				}
				foreach (CustomCompanion companion in allCustomCompanions)
				{
					companion.Initialize();
					companion.Setup();
					string companionType = (companion._isModifyingCompanion) ? "modified" : "new";
					Plugin.Log.LogInfo($"  Successfully loaded {companionType} companion \"{companion.Name}\"! (EntityID:{companion.EntityID})");
				}

			} // end Postfix

		} // end class PatchMainMenuController

		/* Sets the current SO to an already-existing companion */
		public void ModifyExistingCompanion(string name)
		{
			Companion = AssetManager.GetAllLoadedCompanions()[$"{name}_CH"];
			_isModifyingCompanion = true;

		} // end ModifyExistingCompanionSO

		/* Sets the current SO to a COPY of an already-existing companion */
		public void LoadExistingCompanion(string name)
		{
			ModifyExistingCompanion(name);
			Companion = UnityEngine.Object.Instantiate(Companion);
			_isModifyingCompanion = false;
			EntityID = _companionEntityID;

		} // end LoadExistingCompanionSO

	} // end CustomCompanion

} // end namespace EasyCustomStuff