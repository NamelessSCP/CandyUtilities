namespace CandyUtilities;

#if LABAPI
using LabApi.Loader.Features.Plugins;
using LabApi.Loader;
#else
using Exiled.API.Features;
#endif

#if LABAPI
public class CandyUtil : Plugin
#else
public class CandyUtil : Plugin<Config, Translation>
#endif
{
    private EventHandler eventHandler { get; set; } = null!;

    public static CandyUtil Instance { get; private set; } = null!;

    public override string Name => "Candy Utilities";
    public override string Author => "@misfiy";
    public override Version Version => new(1, 0, 10);
    
#if LABAPI
    public override string Description => "Utils for SCP-330";
    public override Version RequiredApiVersion => new(1, 0, 0);
#endif

#if LABAPI
    public Config Config { get; private set; } = null!;
    public Translation Translation { get; private set; } = null!;
#endif

#if LABAPI
    public override void Enable()
#else
    public override void OnEnabled()
#endif
    {
        if (!Config.IsEnabled)
            return;
        
        Instance = this;
        eventHandler = new EventHandler();
    }
#if LABAPI
    public override void LoadConfigs()
    {
        Config = this.TryLoadConfig("config.yml", out Config? config)
            ? config
            : new Config();
        Translation = this.TryLoadConfig("translation.yml", out Translation? translation)
            ? translation
            : new Translation();
    }
#endif

#if LABAPI
    public override void Disable()
#else
    public override void OnDisabled()
#endif
    {
        eventHandler = null!;
        Instance = null!;
    }
}