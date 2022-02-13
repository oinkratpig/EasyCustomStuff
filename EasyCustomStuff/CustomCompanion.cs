using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace EasyCustomStuff
{
    public abstract class CustomCompanion : CustomObject
    {
        private static int _nextEntityID = 100000;
        private static List<CustomCompanion> _allCustomCompanions = new();
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
        protected string Name
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
        protected EntityIDs EntityID
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
        protected bool UsesSlap
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
        protected bool UsesAllAbilities
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
        protected bool UsesOverworldMovingAnim
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

        /* Constructor */
        public CustomCompanion()
        {
            _allCustomCompanions.Add(this);

        } // end constructor

        /* Ran when companion data is created */
        protected abstract void Setup();

        /* Sets the current SO to an already-existing companion */
        protected void ModifyExistingCompanion(string name)
        {
            //Companion = GetAllLoadedCompanions()[$"{name}_CH"];
            Companion = LoadedAssetsHandler.GetCharcater(name + "_CH");
            _isModifyingCompanion = true;

        } // end ModifyExistingCompanion

        /* Sets the current SO to a COPY of an already-existing companion */
        protected void LoadExistingCompanion(string name)
        {
            ModifyExistingCompanion(name);
            Companion = Object.Instantiate(Companion);
            _isModifyingCompanion = false;
            _companionEntityID = (EntityIDs)_nextEntityID++;
            EntityID = _companionEntityID;

        } // end LoadExistingCompanion

        /* On the main menu, create all of the custom companions */
        [HarmonyPatch(typeof(MainMenuController), "Start")]
        class PatchMainMenuController
        {
            [HarmonyPostfix]
            private static void Postfix(MainMenuController __instance)
            {
                Log.LogInfo("Loading custom companions...");
                if (_allCustomCompanions.Count == 0)
                {
                    Log.LogInfo("No companions to load.");
                    return;
                }
                foreach (CustomCompanion companion in _allCustomCompanions)
                {
                    companion.Setup();
                    string companionType = (companion._isModifyingCompanion) ? "modified" : "new";
                    Log.LogInfo($"  Successfully loaded {companionType} companion \"{companion.Name}\"! (EntityID:{companion.EntityID})");
                }

            } // end Postfix

        } // end class PatchMainMenuController

    } // end class CustomCompanion

} // end namespace EasyCustomStuff