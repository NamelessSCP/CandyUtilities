namespace CandyUtilities;

using LabApi.Loader.Features.Plugins;
using LabApi.Loader;

public class CandyUtil : Plugin
{
    private EventHandler eventHandler { get; set; } = null!;

    public static CandyUtil Instance { get; private set; } = null!;

    public override string Name => "Candy Utilities";
    public override string Author => "@misfiy";
    public override Version Version => new(1, 0, 10);

    public override string Description => "Utils for SCP-330";
    public override Version RequiredApiVersion => new(1, 0, 0);

    public Config Config { get; private set; } = null!;
    public Translation Translation { get; private set; } = null!;

    public override void Enable()
    {

        Instance = this;
        eventHandler = new EventHandler();
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
        eventHandler = null!;
        Instance = null!;
    }
}