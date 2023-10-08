namespace CandyUtilities;

using Exiled.API.Features;
using CandyUtilities.Events;
using Scp330 = Exiled.Events.Handlers.Scp330;

public class CandyUtil : Plugin<Config, Translation>
{
     public override string Name => "Candy Utilities";
     public override string Prefix => "CandyUtils";
     public override string Author => "@misfiy";
	public override Version Version => new(1, 0, 4);
	public override Version RequiredExiledVersion => new(8, 2, 1);

     public static CandyUtil Instance;

	private EventHandler eventHandler;
     public override void OnEnabled()
     {
          Instance = this;

          RegisterEvents();
          base.OnEnabled();
     }

     public override void OnDisabled()
     {
          UnregisterEvents();
          Instance = null!;
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
          eventHandler = null!;
     }
}