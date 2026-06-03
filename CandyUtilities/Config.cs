namespace CandyUtilities;

using PlayerRoles;
using System.ComponentModel;

public sealed class Config
{
    /// <summary>
    /// If selected, will use base-game defaults.
    /// </summary>
    public const int SeverLimitDefault = -1;

    /// <summary>
    /// If selected, will allow infinite candies.
    /// </summary>
    public const int SeverLimitInfinite = -2;
    
    [Description("Chance of getting pink candy from bowl")]
    public ushort PinkChance { get; set; } = 3;

    [Description("Set how many candies everyone can take, -1 will result in default behaviour, -2 will be infinite")]
    public int GlobalSeverLimit { get; set; } = SeverLimitDefault;

    [Description("Set how many candies a role needs to pick up before hand is severed - Overrides GlobalSeverLimit, -1 results in default behaviour, -2 will be infinite")]
    public Dictionary<RoleTypeId, int> SeverCounts { get; set; } = new()
    {
        { RoleTypeId.None, 3 },
    };
}