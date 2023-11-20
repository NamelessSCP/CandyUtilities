#pragma warning disable CS8618

namespace CandyUtilities_NWAPI;

using PluginAPI.Core.Attributes;
using HarmonyLib;

public class CandyUtils
{
    private Harmony harmony { get; set; } = new("CandyUtilities");
    
    public static CandyUtils Instance { get; private set; }
    
    [PluginConfig]
    public Config Config;

    [PluginEntryPoint("CandyUtilities", "1.0.7", "Candy Utilities", "@misfiy")]
    void LoadPlugin()
    {
        if (!Config.IsEnabled)
            return;
        Instance = this;
        harmony.PatchAll();
    }
}