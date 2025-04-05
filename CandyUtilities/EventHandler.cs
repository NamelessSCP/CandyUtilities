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
            ev.CandyType = CandyKindID.Pink;

        int maxCandies = config.SeverCounts.TryGetValue(ev.Player.Role, out int count)
            ? count
            : config.GlobalSeverLimit;

        AdjustSeverValues(ev, maxCandies);
    }

    private void OnInteracted(PlayerInteractedScp330EventArgs ev)
    {
        if (ev.AllowPunishment) 
            ev.Player.SendHint(translation.SeveredText, 4);
        else
        {
            string pickupText = translation.CandyText.TryGetValue(ev.CandyType, out string candy)
                ? translation.PickupText.Replace("%type%", candy)
                : string.Empty;
            if (!string.IsNullOrEmpty(pickupText))
                ev.Player.SendHint(pickupText, 4);
        }
    }

    private void AdjustSeverValues(PlayerInteractingScp330EventArgs ev, int maxCandies)
    {
        bool shouldPunish = ev.Uses >= maxCandies;
        ev.AllowPunishment = shouldPunish;

        if (shouldPunish)
            ev.Uses = 2;
    }
}