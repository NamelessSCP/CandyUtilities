#pragma warning disable CS8618

namespace CandyUtilities_NWAPI;

using PluginAPI.Core.Attributes;
using HarmonyLib;

public class CandyUtils
{
    private Harmony harmony;
    
    public static CandyUtils Instance { get; private set; }
    
    [PluginConfig]
    public Config Config;

    [PluginEntryPoint("CandyUtilities", "1.0.10", "Candy Utilities", "@misfiy")]
    void LoadPlugin()
    {
        if (!Config.IsEnabled)
            return;

        Instance = this;
        harmony = new("Misfiy.CandyUtilities" + DateTime.Now);
        harmony.PatchAll();
    }
}