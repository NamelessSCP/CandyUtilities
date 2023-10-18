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

		if(CandyUtil.Instance.Config.SeverCounts.TryGetValue(ev.Player.Role.Type, out int max))
		{
			ev.ShouldSever = ev.UsageCount >= max;
			Log.Debug($"Usage ({ev.UsageCount}/{max}) - ShouldSever ({ev.ShouldSever})");
		}

		if (!CandyUtil.Instance.Translation.PickupText.IsEmpty())
		{
			string text = CandyUtil.Instance.Translation.PickupText.Replace("%type%", CandyUtil.Instance.Translation.CandyText[ev.Candy]);
			ev.Player.ShowHint(text);
			Log.Debug($"Text shown to {ev.Player.Nickname}: {text}");
		}
	}
}