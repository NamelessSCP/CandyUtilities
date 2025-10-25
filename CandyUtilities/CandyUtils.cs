namespace CandyUtilities;

using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

public class CandyUtil : Plugin
{
    private EventHandler? _eventHandler;

    public static CandyUtil Instance { get; private set; } = null!;

    public override string Name => "Candy Utilities";
    public override string Author => "@misfiy";
    public override Version Version => new(2, 0, 1);
    public override string Description => "Utils for SCP-330";
    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

    public Config Config { get; private set; } = null!;
    public Translation Translation { get; private set; } = null!;

    public override void Enable()
    {
        Instance = this;
        _eventHandler = new EventHandler();
    }

    public override void LoadConfigs()
    {
        Config = this.TryLoadConfig("config.yml", out Config? config)
            ? config
            : new Config();
        Translation = this.TryLoadConfig("translation.yml", out Translation? translation)
            ? translation
            : new Translation();
    }

    public override void Disable()
    {
        _eventHandler = null;
        Instance = null!;
    }
}