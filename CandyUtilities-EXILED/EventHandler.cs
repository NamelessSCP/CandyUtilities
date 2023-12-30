namespace CandyUtilities_EXILED;

using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using Exiled.API.Features;
using UnityEngine;

public class EventHandler
{
    private static Config config => CandyUtil.Instance.Config;
    private static Translation translation => CandyUtil.Instance.Translation;
    
    public EventHandler()
    {
        Exiled.Events.Handlers.Scp330.InteractingScp330 += OnInteraction;
    }

    ~EventHandler()
    {
        Exiled.Events.Handlers.Scp330.InteractingScp330 -= OnInteraction;
    }
    
    public void OnInteraction(InteractingScp330EventArgs ev)
    {
        if (!ev.IsAllowed) return;

        if (Random.Range(1, 100) <= config.PinkChance)
        {
            ev.Candy = CandyKindID.Pink;
            Log.Debug("Pink candy has been selected!");
        }
        
        string pickupText = translation.CandyText.TryGetValue(ev.Candy, out string? candy)
            ? translation.PickupText.Replace("%type%", candy)
            : string.Empty;
        int maxCandies = config.SeverCounts.TryGetValue(ev.Player.Role.Type, out int count)
            ? count
            : config.GlobalSeverLimit;

        ev.ShouldSever = ev.UsageCount >= maxCandies;
        Log.Debug($"Usage ({ev.UsageCount}/{maxCandies}) - ShouldSever ({ev.ShouldSever})");
		
        if (ev.ShouldSever && !translation.SeveredText.IsEmpty())
            ev.Player.ShowHint(translation.SeveredText, 4);
        else if (!pickupText.IsEmpty())
            ev.Player.ShowHint(pickupText, 4);
    }
}