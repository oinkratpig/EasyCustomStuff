using BepInEx;
using HarmonyLib;

namespace EasyCustomStuff
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony _harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            _harmony.PatchAll();

            // Instantiate custom characters
            ExampleCompanion exampleCompanion = new();

        } // end Awake

    } // end class Plugin

} // end namespace EasyCustomStuff