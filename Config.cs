using Exiled.API.Interfaces;
using System.ComponentModel;

namespace CandyUtilities
{
     public sealed class Config : IConfig
     {
          public bool IsEnabled { get; set; } = true;
          public bool Debug { get; set; } = false;
          [Description("Chance of getting pink candy from bowl")]
          public ushort PinkChance { get; set; } = 2;
          [Description("Whether or not to show a hint on picking up candy")]
          public bool ShouldShowHint { get; set; } = true;
          [Description("The text shown when picking up a candy, note %type% gets replaced with the type of candy. If ShouldShowHint is false then this won't do anything.")]
          public string TextOnPickup { get; set; } = "You take a piece of %type% candy.";
     }
}