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

        Footprint footprint = new(ply);
        float num = 0.1f;
        int num2 = 0;
        foreach (Footprint foot in __instance._takenCandies)
        {
            if (foot.SameLife(footprint))
            {
                num = Mathf.Min(num, (float)foot.Stopwatch.Elapsed.TotalSeconds);
                num2++;
            }
        }

        if (num < 0.1f)
            return false;
        
        if (Scp330Bag.ServerProcessPickup(ply, null, out Scp330Bag bag))
        {
            PlayerInteractScp330Event playerInteractScp330Event = new PlayerInteractScp330Event(ply, num2);
            if (!EventManager.ExecuteEvent(playerInteractScp330Event))
                return false;
            
            if (playerInteractScp330Event.PlaySound)
                __instance.RpcMakeSound();
            if (!CandyUtils.Instance.Config.RoleSeverCounts.TryGetValue(ply.GetRoleId(), out int maxUses))
                maxUses = CandyUtils.Instance.Config.GlobalSeverLimit;
            
            if (playerInteractScp330Event.AllowPunishment && playerInteractScp330Event.Uses >= maxUses)
            {
                if(!CandyUtils.Instance.Config.SeveredText.IsEmpty())
                    Player.Get(ply).ReceiveHint(CandyUtils.Instance.Config.SeveredText);
                
                ply.playerEffectsController.EnableEffect<SeveredHands>();
                while (__instance._takenCandies.Remove(footprint)) {}

                return false;
            }
            __instance._takenCandies.Add(footprint);
        }

        return false;
    }
}