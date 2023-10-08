namespace CandyUtilities;

using Exiled.API.Interfaces;
using System.ComponentModel;

public sealed class Config : IConfig
{
     public bool IsEnabled { get; set; } = true;
     public bool Debug { get; set; } = false;
     [Description("Chance of getting pink candy from bowl")]
     public ushort PinkChance { get; set; } = 2;
}