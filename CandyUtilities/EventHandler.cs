namespace CandyUtilities;

#if LABAPI
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
#endif
#if EXILED
using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp330;
#endif

using InventorySystem.Items.Usables.Scp330;
using UnityEngine;

public class EventHandler
{
    private static Config config => CandyUtil.Instance.Config;
    private static Translation translation => CandyUtil.Instance.Translation;
    
    internal EventHandler()
    {
#if LABAPI
        LabApi.Events.Handlers.PlayerEvents.InteractingScp330 += OnInteraction;
#endif
#if EXILED
        Exiled.Events.Handlers.Scp330.InteractingScp330 += OnInteraction;
#endif
    }

    ~EventHandler()
    {
#if LABAPI
        LabApi.Events.Handlers.PlayerEvents.InteractingScp330 -= OnInteraction;
#endif
#if EXILED
        Exiled.Events.Handlers.Scp330.InteractingScp330 -= OnInteraction;
#endif
    }
    
#if LABAPI
    private void OnInteraction(PlayerInteractingScp330EventArgs ev)
#endif
#if EXILED
    private void OnInteraction(InteractingScp330EventArgs ev)
#endif
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

        SendHint(ev.Player, ev.ShouldSever ? translation.SeveredText : pickupText);
    }

    private void SendHint(Player player, string hint)
    {
        if (string.IsNullOrEmpty(hint))
            return;
        
#if LABAPI
        player.SendHint(hint, 4);
#endif
#if EXILED
        player.ShowHint(hint, 4);
#endif
    }
}