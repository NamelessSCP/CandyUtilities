namespace CandyUtilities;

using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

public sealed class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    [Description("Chance of getting pink candy from bowl")]
    public ushort PinkChance { get; set; } = 3;
    [Description("Set how many candies everyone can take")]
    public int GlobalSeverLimit { get; set; } = 2;
    [Description("Set how many candies a role needs to pick up before hand is severed - Overrides GlobalSeverLimit")]
    public Dictionary<RoleTypeId, int> SeverCounts { get; set; } = new()
    {
        { RoleTypeId.Tutorial, 3 },
    };
}