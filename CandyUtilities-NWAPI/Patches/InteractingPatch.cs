namespace CandyUtilities_NWAPI.Patches;

using PluginAPI.Events;
using InventorySystem.Items.Usables.Scp330;
using UnityEngine;
using InventorySystem.Items;
using Interactables.Interobjects;
using PlayerRoles;
using Footprinting;
using CustomPlayerEffects;
using PluginAPI.Core;

[HarmonyLib.HarmonyPatch(typeof(Scp330Interobject), nameof(Scp330Interobject.ServerInteract))]
internal static class InteractingPatch
{
    private static bool Prefix(Scp330Interobject __instance, ReferenceHub ply)
    {
        if (!ply.IsHuman() || ply.HasBlock(BlockedInteraction.GrabItems))
            return false;

        float lastTaken = Scp330Interobject.TakeCooldown;
        int prevUses = 0;
        foreach (Footprint previousUse in __instance._previousUses)
        {
            if (previousUse.LifeIdentifier != ply.roleManager.CurrentRole.UniqueLifeIdentifier)
                continue;
            
            double totalSeconds = previousUse.Stopwatch.Elapsed.TotalSeconds;
            lastTaken = Mathf.Min(lastTaken, (float) totalSeconds);
            ++prevUses;
        }

        if (lastTaken < Scp330Interobject.TakeCooldown)
            return false;

        if (!Scp330Bag.ServerProcessPickup(ply, null, out Scp330Bag _))
            return false;

        PlayerInteractScp330Event ev = new PlayerInteractScp330Event(ply, prevUses);
        if (!EventManager.ExecuteEvent(ev))
            return false;

        if (ev.PlaySound)
            __instance.RpcMakeSound();

        if (!CandyUtils.Instance.Config.RoleSeverCounts.TryGetValue(ply.GetRoleId(), out int maxUses))
            maxUses = CandyUtils.Instance.Config.GlobalSeverLimit;

        if (ev.AllowPunishment && ev.Uses >= maxUses)
        {
            if(!CandyUtils.Instance.Config.SeveredText.IsEmpty())
                Player.Get(ply).ReceiveHint(CandyUtils.Instance.Config.SeveredText);

            ply.playerEffectsController.EnableEffect<SeveredHands>();
            __instance.ClearUsesForRole(ply.roleManager.CurrentRole);
        }
        else
            __instance._previousUses.Add(new Footprint(ply));

        return false;
    }
}