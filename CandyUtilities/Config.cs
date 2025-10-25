namespace CandyUtilities;

using PlayerRoles;
using System.ComponentModel;

public sealed class Config
{
    /// <summary>
    /// Defines the Integer that should be considered default of SCP:SL candies to take before severing.
    /// </summary>
    public const int SeverLimitDefault = -1;
    
    [Description("Chance of getting pink candy from bowl")]
    public ushort PinkChance { get; set; } = 3;

    [Description("Set how many candies everyone can take, -1 will result in default behaviour")]
    public int GlobalSeverLimit { get; set; } = SeverLimitDefault;

    [Description("Set how many candies a role needs to pick up before hand is severed - Overrides GlobalSeverLimit, -1 results in default behaviour")]
    public Dictionary<RoleTypeId, int> SeverCounts { get; set; } = new()
    {
        { RoleTypeId.None, 3 },
    };
}