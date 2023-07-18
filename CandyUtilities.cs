namespace CandyUtilities
{
     using Exiled.API.Features;
     using Exiled.API.Enums;
     using CandyUtilities.Events;
     using Scp330 = Exiled.Events.Handlers.Scp330;

     public class CandyUtil : Plugin<Config>
     {
          public override string Name => "Candy Utilities";
          public override string Prefix => "CandyUtils";
          public override string Author => "@misfiy";
          public override PluginPriority Priority => PluginPriority.Default;
          private EventHandler eventHandler;
          public static CandyUtil Instance;
          private Config config;
          public override void OnEnabled()
          {
               Instance = this;
               config = Instance.Config;

               RegisterEvents();
               base.OnEnabled();
          }

          public override void OnDisabled()
          {
               UnregisterEvents();
               Instance = null;
               base.OnDisabled();
          }
          public void RegisterEvents()
          {
               eventHandler = new EventHandler();
               Scp330.InteractingScp330 += eventHandler.OnInteraction;

               Log.Debug("Events have been registered!");
          }
          public void UnregisterEvents()
          {
               Scp330.InteractingScp330 -= eventHandler.OnInteraction;
               eventHandler = null;
          }
     }
}

// player.TryAddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);