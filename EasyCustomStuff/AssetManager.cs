using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace EasyCustomStuff
{
	class AssetManager
	{
		/* Returns the dictionary containing all loaded base characters */
		public static Dictionary<string, CharacterSO> GetAllLoadedCompanions()
		{
			// Keys: "CompanionName_CH"
			return Traverse.Create(typeof(LoadedAssetsHandler))
				.Field("LoadedCharacters")
				.GetValue<Dictionary<string, CharacterSO>>();

		} // end GetLoadedCompanions

	} // end class AssetManager

} // end namespace EasyCustomStuff