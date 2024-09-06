using System.ComponentModel;

namespace P3R.Gameplay.Reloaded.Services.Difficulty;

public class DifficultyMultipliers
{
    [DisplayName("Enemy Damage")]
    [DefaultValue(100)]
    public int DamageRateToEnemy { get; set; } = 100;

    [DisplayName("Player Damage")]
    [DefaultValue(100)]
    public int DamageRateToPlayer { get; set; } = 100;

    [DisplayName("Experience Rate")]
    [DefaultValue(100)]
    public int ExpRate { get; set; } = 100;

    [DisplayName("Enemy Weakness Damage")]
    [DefaultValue(100)]
    public int DamageRateToEnemyWeak { get; set; } = 100;

    [DisplayName("Player Weakness Damage")]
    [DefaultValue(100)]
    public int DamageRateToPlayerWeak { get; set; } = 100;

    [DisplayName("Enemy Critical Damage")]
    [DefaultValue(100)]
    public int DamageRateToEnemyCritical { get; set; } = 100;

    [DisplayName("Player Critical Damage")]
    [DefaultValue(100)]
    public int DamageRateToPlayerCritical { get; set; } = 100;

    [DisplayName("Materials Money")]
    [DefaultValue(100)]
    public int MoneyRateToMaterials { get; set; } = 100;

    [DisplayName("Enemy Debuff Accuracy")]
    [DefaultValue(100)]
    public int BadStatusHitRateFromEnemy { get; set; } = 100;

    [DisplayName("Player Debuff Accuracy")]
    [DefaultValue(100)]
    public int BadStatusHitRateFromPlayer { get; set; } = 100;
}
