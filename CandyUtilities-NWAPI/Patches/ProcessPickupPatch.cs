using InventorySystem.Items;

namespace CandyUtilities_NWAPI.Patches;

using InventorySystem.Items.Usables.Scp330;
using InventorySystem;
using UnityEngine;
using PluginAPI.Core;

[HarmonyLib.HarmonyPatch(typeof(Scp330Bag), nameof(Scp330Bag.ServerProcessPickup))]
internal static class ProcessPickupPatch
{
    private static bool Prefix(ReferenceHub ply, Scp330Pickup pickup, out Scp330Bag bag, ref bool __result)
    {
        if (!Scp330Bag.TryGetBag(ply, out bag))
        {
            int num = pickup == null ? 0 : pickup.Info.Serial;
            __result = ply.inventory.ServerAddItem(ItemType.SCP330, ItemAddReason.Scp914Upgrade, (ushort)num, pickup) != null;
            return false;
        }

        bool res = false;
        
        if (pickup == null)
        {
            CandyKindID candy = Scp330Candies.GetRandom();
            if (Random.Range(0, 100) <= CandyUtils.Instance.Config.PinkChance) 
                candy = CandyKindID.Pink;
            
            if (!CandyUtils.Instance.Config.PickupText.IsEmpty() && CandyUtils.Instance.Config.CandyText.TryGetValue(candy, out string candyText))
            {
                Player.Get(ply).ReceiveHint(CandyUtils.Instance.Config.PickupText.Replace("%type%", candyText));   
            }

            res = bag.TryAddSpecific(candy);
        }
        else
        {
            while (pickup.StoredCandies.Count > 0 && bag.TryAddSpecific(pickup.StoredCandies[0]))
            {
                res = true;
                pickup.StoredCandies.RemoveAt(0);
            }
        }
        if (bag.AcquisitionAlreadyReceived)
            bag.ServerRefreshBag();

        __result = res;
        return false;
    }
}