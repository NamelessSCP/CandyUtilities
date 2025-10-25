namespace CandyUtilities;

using LabApi.Events.Arguments.PlayerEvents;
using InventorySystem.Items.Usables.Scp330;
using UnityEngine;

public class EventHandler
{
    private static Config _config => CandyUtil.Instance.Config;
    private static Translation _translation => CandyUtil.Instance.Translation;
    
    internal EventHandler()
    {
        LabApi.Events.Handlers.PlayerEvents.InteractingScp330 += OnInteraction;
        LabApi.Events.Handlers.PlayerEvents.InteractedScp330 += OnInteracted;
    }

    ~EventHandler()
    {
        LabApi.Events.Handlers.PlayerEvents.InteractingScp330 -= OnInteraction;
        LabApi.Events.Handlers.PlayerEvents.InteractedScp330 -= OnInteracted;
    }
    
    private void OnInteraction(PlayerInteractingScp330EventArgs ev)
    {
        if (!ev.IsAllowed)
            return;

        if (Random.Range(1, 100) <= _config.PinkChance)
            ev.CandyType = CandyKindID.Pink;

        int maxCandies = _config.SeverCounts.TryGetValue(ev.Player.Role, out int count)
            ? count
            : _config.GlobalSeverLimit;

        if (maxCandies == Config.SeverLimitDefault)
            return;

        AdjustSeverValues(ev, maxCandies);
    }

    private void OnInteracted(PlayerInteractedScp330EventArgs ev)
    {
        if (ev.AllowPunishment)
        {
            if (!string.IsNullOrEmpty(_translation.SeveredText))
                ev.Player.SendHint(_translation.SeveredText, 4);
            return;
        }

        string pickupText = _translation.CandyText.TryGetValue(ev.CandyType, out string candy)
            ? _translation.PickupText.Replace("%type%", candy)
            : string.Empty;
        if (!string.IsNullOrEmpty(pickupText))
            ev.Player.SendHint(pickupText, 4);
    }

    private void AdjustSeverValues(PlayerInteractingScp330EventArgs ev, int maxCandies)
    {
        bool shouldPunish = ev.Uses >= maxCandies;
        ev.AllowPunishment = shouldPunish;

        if (shouldPunish)
            ev.Uses = 2;
    }
}