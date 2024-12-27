﻿namespace CandyUtilities_EXILED;

using Exiled.API.Features;

public class CandyUtil : Plugin<Config, Translation>
{
    public override string Name => "Candy Utilities";
    public override string Prefix => "CandyUtils";
    public override string Author => "@misfiy";
    public override Version Version => new(1, 0, 10);
    public override Version RequiredExiledVersion => new(9, 0, 0);

    public static CandyUtil Instance { get; private set; } = null!;

    private EventHandler eventHandler { get; set;  } = null!;
    
    public override void OnEnabled()
    {
        Instance = this;
        eventHandler = new();

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        eventHandler = null!;
          
        Instance = null!;
        base.OnDisabled();
    }
}