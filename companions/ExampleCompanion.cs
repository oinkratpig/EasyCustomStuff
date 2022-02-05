using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCustomStuff
{
	class ExampleCompanion : CustomCompanion
	{
		public override void Setup()
		{
			ModifyExistingCompanion("Anton");
			Name = "Dummy";
			UsesSlap = false;
			UsesAllAbilities = true;
			UsesOverworldMovingAnim = false;

		} // end Create

	} // end class ExampleCompanion

} // end namespace EasyCustomStuff