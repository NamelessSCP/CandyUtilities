namespace CandyUtilities;

using LabApi.Events.Arguments.PlayerEvents;
using InventorySystem.Items.Usables.Scp330;
using UnityEngine;

public class EventHandler
{
    private static Config config => CandyUtil.Instance.Config;
    private static Translation translation => CandyUtil.Instance.Translation;
    
    internal EventHandler()
    {
        LabApi.Events.Handlers.PlayerEvents.InteractingScp330 += OnInteraction;
    }

    ~EventHandler()
    {
        LabApi.Events.Handlers.PlayerEvents.InteractingScp330 -= OnInteraction;
    }
    
    private void OnInteraction(PlayerInteractingScp330EventArgs ev)
    {
        if (!ev.IsAllowed)
            return;

        if (Random.Range(1, 100) <= config.PinkChance)
            ev.Candy = CandyKindID.Pink;
        
        string pickupText = translation.CandyText.TryGetValue(ev.Candy, out string candy)
            ? translation.PickupText.Replace("%type%", candy)
            : string.Empty;

        int maxCandies = config.SeverCounts.TryGetValue(ev.Player.Role, out int count)
            ? count
            : config.GlobalSeverLimit;

        ev.ShouldSever = ev.UsageCount >= maxCandies;
        
        if (!string.IsNullOrEmpty(ev.ShouldSever ? translation.SeveredText : pickupText))
            ev.Player.SendHint(ev.ShouldSever ? translation.SeveredText : pickupText, 4);
    }
}