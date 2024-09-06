using P3R.Gameplay.Reloaded.Services.Difficulty;
using P3R.Gameplay.Reloaded.Template.Configuration;
using System.ComponentModel;

namespace P3R.Gameplay.Reloaded.Configuration;

public class Config : Configurable<Config>
{
    [DisplayName("Log Level")]
    [DefaultValue(LogLevel.Information)]
    public LogLevel LogLevel { get; set; } = LogLevel.Information;

    [DisplayName("Output Data to File")]
    [Description("Dumps original data to YAML files for viewing or editing.")]
    [DefaultValue(false)]
    public bool IsDumpEnabled { get; set; } = false;

    [DisplayName("Difficulty Multipliers %")]
    public DifficultyMultipliers Difficulty { get; set; } = new();
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}
