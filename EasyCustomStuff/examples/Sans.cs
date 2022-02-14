using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EasyCustomStuff
{
    public class Sans : CustomCompanion
    {
		protected override void Setup()
		{
			// Debug
			ModifyExistingCompanion("Nowak");
			UsesSlap = true;

			// Base stats
			ModifyExistingCompanion("Gospel");
			Name = "Sans";
			UsesSlap = true;
			UsesAllAbilities = true;
			UsesOverworldMovingAnim = false;

			// No passives
			Companion.passiveAbilities = new BasePassiveAbilitySO[0];

			// Load bimini
			CharacterSO oldCompanion = Companion;
			ModifyExistingCompanion("Bimini");
			oldCompanion.basicCharAbility.ability = Companion.rankedData[3].rankAbilities[0].ability;
			var newAbility = Companion.rankedData[0].rankAbilities[1].ability;

			// New target
			Targetting_BySlot_Index newTarget = ScriptableObject.CreateInstance<Targetting_BySlot_Index>();
			newTarget.slotPointerDirections = new int[5]{ -2, -1, 0, 1, 2 };

			// Attacks twice
			EffectInfo test = newAbility.effects[0];
			test.entryVariable = 1;
			test.targets = newTarget;
			newAbility.effects = new EffectInfo[8].Populate(test);

			// New effect
			ApplyOilSlickedEffect ab = ScriptableObject.CreateInstance<ApplyOilSlickedEffect>();
			EffectInfo ei = new EffectInfo();
			ei.effect = ab;
			ei.targets = test.targets;
			ei.entryVariable = 100;
			newAbility.effects[0] = ei;

			// New new effect
			ApplyRupturedEffect ab2 = ScriptableObject.CreateInstance<ApplyRupturedEffect>();
			EffectInfo ei2 = new EffectInfo();
			ei2.effect = ab2;
			ei2.targets = test.targets;
			ei2.entryVariable = 100;
			newAbility.effects[1] = ei2;

			// New new effect
			/*
			CharacterSO _oldCompanion = Companion;
			ModifyExistingCompanion("SmokeStacks");
			var removePigmentEffect = Companion.
			*/

			RemoveOverflowManaEffect ab3 = ScriptableObject.CreateInstance<RemoveOverflowManaEffect>();
			EffectInfo ei3 = new EffectInfo();
			ei3.effect = ab3;
			ei3.targets = test.targets;
			ei3.entryVariable = 0;
			Log.LogInfo(Traverse.Create(ei3));
            Traverse.Create(ab3).Field("_fullyDeplete").SetValue(true);
            newAbility.effects[7] = ei3;

			// Back to Gospel
			Companion = oldCompanion;

			// Set second ability
			Companion.rankedData[0].rankAbilities[1].ability = newAbility;

			// New animation
			EnemySO enemy = LoadedAssetsHandler.GetEnemy("Visage_MyOwn_EN");
			newAbility.visuals = enemy.abilities[0].ability.visuals;

			// ...
			BaseBundleGeneratorSO bundle = LoadedAssetsHandler.GetEnemyBundle("Zone01_Mung_Easy_EnemyBundle");
			RandomEnemyBundleSO randomBundle = bundle as RandomEnemyBundleSO;
			RandomEnemyGroup group = new();
			Traverse.Create(group).Field("_enemyNames").SetValue(new string[] { "Roids_BOSS", "Bronzo_EN" });
			Traverse.Create(randomBundle).Field("_enemyBundles").SetValue(new RandomEnemyGroup[] { group });

			// ...
			// Companion.extraCombatSprites;

			// Create passive
			// Whenever damaged, cancel damage and move left or right

			// Create passive
			// No incoming direct damage

		} // end Setup

	} // end class Sans

} // end namespace EasyCustomStuff