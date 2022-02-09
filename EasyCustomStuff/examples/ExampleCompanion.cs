using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace EasyCustomStuff
{
	class ExampleCompanion : CustomCompanion
	{
		protected override void Setup()
		{
			ModifyExistingCompanion("Gospel");
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

		} // end Setup

	} // end class ExampleCompanion

} // end namespace EasyCustomStuff