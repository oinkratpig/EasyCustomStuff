using BepInEx;
using HarmonyLib;

namespace EasyCustomStuff
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource Log;
        private readonly Harmony _harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            // Plugin startup logic
            Log = Logger;
            Log.LogInfo($"EasyCustomStuff is now loaded.");
            _harmony.PatchAll();

            // Add custom characters
            ExampleCompanion exampleCompanion = new();

        } // end Awake

    } // end class Plugin

} // end namespace EasyCustomStuff