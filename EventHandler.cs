namespace CandyUtilities.Events;

using CandyUtilities;
using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using Exiled.API.Features;

public sealed class EventHandler
{
     private readonly Config config = CandyUtil.Instance.Config;
     private readonly Random random = new();
     private readonly Dictionary<string, string> candies = new()
     {
          { "Rainbow", "<color=#FF0000>R</color><color=#FF7F00>a</color><color=#FFFF00>i</color><color=#00FF00>n</color><color=#0000FF>b</color><color=#4B0082>o</color><color=#8A2BE2>w</color>" },
          { "Yellow", "<color=#FFFF00>Yellow</color>" },
          { "Purple", "<color=#800080>Purple</color>" },
          { "Red", "<color=#FF0000>Red</color>" },
          { "Green", "<color=#008000>Green</color>" },
          { "Blue", "<color=#0000FF>Blue</color>" },
          { "Pink", "<color=#FFC0CB>Pink</color>" }
     };

     public void OnInteraction(InteractingScp330EventArgs ev)
     {
          if (!ev.IsAllowed) return;
          if (random.Next(1, 101) <= config.PinkChance)
          {
               Log.Debug("Pink candy has been selected!");
               ev.Candy = CandyKindID.Pink;
          }
          if (!config.ShouldShowHint || config.TextOnPickup.Length == 0) return;
          string currentCandy = candies[ev.Candy.ToString()];
          string text = config.TextOnPickup.Replace("%type%", currentCandy);
          ev.Player.ShowHint(text);
          Log.Debug("Candy picked up: " + text);
     }
}