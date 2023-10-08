namespace CandyUtilities.Events;

using CandyUtilities;
using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using Exiled.API.Features;

public sealed class EventHandler
{
     private readonly Random random = new();

     public void OnInteraction(InteractingScp330EventArgs ev)
     {
          if (!ev.IsAllowed) return;
          if (random.Next(1, 101) <= CandyUtil.Instance.Config.PinkChance)
          {
               Log.Debug("Pink candy has been selected!");
               ev.Candy = CandyKindID.Pink;
          }

          if (CandyUtil.Instance.Translation.PickupText.IsEmpty()) return;
          string currentCandy = CandyUtil.Instance.Translation.CandyText[ev.Candy];
          string text = CandyUtil.Instance.Translation.PickupText.Replace("%type%", currentCandy);
          ev.Player.ShowHint(text);
          Log.Debug("Candy picked up:\n " + text);
     }
}