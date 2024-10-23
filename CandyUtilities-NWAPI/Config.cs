namespace CandyUtilities_NWAPI;

using System.ComponentModel;
using InventorySystem.Items.Usables.Scp330;
using PlayerRoles;

public class Config
{
    public bool IsEnabled { get; set; } = true;
    public ushort PinkChance { get; set; } = 3;
    public int GlobalSeverLimit { get; set; } = 2;
    public Dictionary<RoleTypeId, int> RoleSeverCounts { get; set; } = new()
    {
        { RoleTypeId.Tutorial, 3 }
    };

    [Description("Translations:")]
    public string PickupText { get; set; } = "You take a piece of %type% candy.";
    public Dictionary<CandyKindID, string> CandyText { get; set; } = new()
    {
        { CandyKindID.Rainbow, "<color=#FF0000>R</color><color=#FF7F00>a</color><color=#FFFF00>i</color><color=#00FF00>n</color><color=#0000FF>b</color><color=#4B0082>o</color><color=#8A2BE2>w</color>" },
        { CandyKindID.Yellow, "<color=#FFFF00>Yellow</color>" },
        { CandyKindID.Purple, "<color=#800080>Purple</color>" },
        { CandyKindID.Red, "<color=#FF0000>Red</color>" },
        { CandyKindID.Green, "<color=#008000>Green</color>" },
        { CandyKindID.Blue, "<color=#0000FF>Blue</color>" },
        { CandyKindID.Pink, "<color=#FFC0CB>Pink</color>" },
    };
    public string SeveredText { get; set; } = "Hippity hoppity, your hands are now my property";
}