using P3R.Gameplay.Reloaded.Configuration;
using P3R.Gameplay.Reloaded.Services.Difficulty;
using P3R.Gameplay.Reloaded.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Unreal.ObjectsEmitter.Interfaces;

namespace P3R.Gameplay.Reloaded;

public class Mod : ModBase
{
    private readonly IModLoader modLoader;
    private readonly IReloadedHooks? hooks;
    private readonly ILogger log;
    private readonly IMod owner;

    private Config config;
    private readonly IModConfig modConfig;
    private readonly DifficultyService difficulty;

    public Mod(ModContext context)
    {
        this.modLoader = context.ModLoader;
        this.hooks = context.Hooks;
        this.log = context.Logger;
        this.owner = context.Owner;
        this.config = context.Configuration;
        this.modConfig = context.ModConfig;

        Project.Init(this.modConfig, this.modLoader, this.log, true);
        Log.LogLevel = this.config.LogLevel;

        this.modLoader.GetController<IDataTables>().TryGetTarget(out var dt);

        this.difficulty = new DifficultyService(dt!, this.config);
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        Log.Information("Config Updated: Applying");
        this.config = configuration;

        Log.LogLevel = this.config.LogLevel;
        this.difficulty.ApplyConfig(this.config);
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}