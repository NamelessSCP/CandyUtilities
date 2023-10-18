namespace CandyUtilities;

using Exiled.API.Interfaces;
using PlayerRoles;
using System.ComponentModel;

public sealed class Config : IConfig
{
     public bool IsEnabled { get; set; } = true;
     public bool Debug { get; set; } = false;
     [Description("Chance of getting pink candy from bowl")]
     public ushort PinkChance { get; set; } = 2;
     [Description("Set how many candies a role needs to pick up before hand is severed")]
     public Dictionary<RoleTypeId, int> SeverCounts = new()
     {
          { RoleTypeId.Tutorial, 4 },
     };
}