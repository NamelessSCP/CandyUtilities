namespace CandyUtilities;

#if EXILED
using Exiled.API.Interfaces;
#endif

using PlayerRoles;
using System.ComponentModel;

#if LABAPI
public sealed class Config
#else
public sealed class Config : IConfig
#endif
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