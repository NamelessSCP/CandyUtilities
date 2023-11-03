namespace CandyUtilities_EXILED;

using Exiled.API.Features;
using Scp330 = Exiled.Events.Handlers.Scp330;

public class CandyUtil : Plugin<Config, Translation>
{
    public override string Name => "Candy Utilities";
    public override string Prefix => "CandyUtils";
    public override string Author => "@misfiy";
    public override Version Version => new(1, 0, 6);
    public override Version RequiredExiledVersion => new(8, 3, 5);

    public static CandyUtil Instance { get; private set; } = null!;

    private EventHandler eventHandler { get; set;  } = null!;
    public override void OnEnabled()
    {
        Instance = this;

        eventHandler = new EventHandler();
        Scp330.InteractingScp330 += eventHandler.OnInteraction;
          
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Scp330.InteractingScp330 -= eventHandler.OnInteraction;
        eventHandler = null!;
          
        Instance = null!;
        base.OnDisabled();
    }
}