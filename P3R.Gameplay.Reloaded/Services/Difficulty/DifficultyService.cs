using P3R.Gameplay.Reloaded.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using Unreal.ObjectsEmitter.Interfaces;
using Unreal.ObjectsEmitter.Interfaces.Types;

namespace P3R.Gameplay.Reloaded.Services.Difficulty;

public unsafe class DifficultyService
{
    private Config config;
    private DataTable<FBtlCalcParam>? table;
    private Dictionary<string, FBtlCalcParam> ogDiffParams = [];

    public DifficultyService(IDataTables dt, Config config)
    {
        this.config = config;
        dt.FindDataTable<FBtlCalcParam>("DT_BtlDifficultyParam", table =>
        {
            this.table = table;
            foreach (var pair in table)
            {
                ogDiffParams[pair.Key] = pair.Value;
            }

            this.UpdateDifficulty();
            this.DumpData();
        });
    }

    public void ApplyConfig(Config config)
    {
        this.config = config;
        this.UpdateDifficulty();
        this.DumpData();
    }

    private void UpdateDifficulty()
    {
        if (this.table == null)
        {
            return;
        }

        foreach (var item in this.table)
        {
            var ogParams = this.ogDiffParams[item.Key];

            var newParams = new FBtlCalcParam
            {
                DamageRateToEnemy = (float)this.config.Difficulty.DamageRateToEnemy / 100 * ogParams.DamageRateToEnemy,
                DamageRateToPlayer = (float)this.config.Difficulty.DamageRateToPlayer / 100 * ogParams.DamageRateToPlayer,
                ExpRate = (float)this.config.Difficulty.ExpRate / 100 * ogParams.ExpRate,
                DamageRateToEnemyWeak = (float)this.config.Difficulty.DamageRateToEnemyWeak / 100 * ogParams.DamageRateToEnemyWeak,
                DamageRateToPlayerWeak = (float)this.config.Difficulty.DamageRateToPlayerWeak / 100 * ogParams.DamageRateToPlayerWeak,
                DamageRateToEnemyCritical = (float)this.config.Difficulty.DamageRateToEnemyCritical / 100 * ogParams.DamageRateToEnemyCritical,
                DamageRateToPlayerCritical = (float)this.config.Difficulty.DamageRateToPlayerCritical / 100 * ogParams.DamageRateToPlayerCritical,
                MoneyRateToMaterials = (float)this.config.Difficulty.MoneyRateToMaterials / 100 * ogParams.MoneyRateToMaterials,
                BadStatusHitRateFromEnemy = (float)this.config.Difficulty.BadStatusHitRateFromEnemy / 100 * ogParams.BadStatusHitRateFromEnemy,
                BadStatusHitRateFromPlayer = (float)this.config.Difficulty.BadStatusHitRateFromPlayer / 100 * ogParams.BadStatusHitRateFromPlayer
            };

            this.table[item.Key] = newParams;
        }

        var sb = new StringBuilder();
        sb.AppendLine("Difficulty Multipliers");
        sb.AppendLine($"{nameof(FBtlCalcParam.DamageRateToEnemy)}: {this.config.Difficulty.DamageRateToEnemy}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.DamageRateToPlayer)}: {this.config.Difficulty.DamageRateToPlayer}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.ExpRate)}: {this.config.Difficulty.ExpRate}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.DamageRateToEnemyWeak)}: {this.config.Difficulty.DamageRateToEnemyWeak}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.DamageRateToPlayerWeak)}: {this.config.Difficulty.DamageRateToPlayerWeak}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.DamageRateToEnemyCritical)}: {this.config.Difficulty.DamageRateToEnemyCritical}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.DamageRateToPlayerCritical)}: {this.config.Difficulty.DamageRateToPlayerCritical}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.MoneyRateToMaterials)}: {this.config.Difficulty.MoneyRateToMaterials}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.BadStatusHitRateFromEnemy)}: {this.config.Difficulty.BadStatusHitRateFromEnemy}%");
        sb.AppendLine($"{nameof(FBtlCalcParam.BadStatusHitRateFromPlayer)}: {this.config.Difficulty.BadStatusHitRateFromPlayer}%");
        Log.Debug($"\n{sb}");
    }

    private void DumpData()
    {
        if (this.config.IsDumpEnabled)
        {
            throw new NotImplementedException();
        }
    }

    private void PrintOgNewParams(string diffName, FBtlCalcParam ogParams, FBtlCalcParam newParams)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{nameof(newParams.DamageRateToEnemy)} || {ogParams.DamageRateToEnemy} || {newParams.DamageRateToEnemy}");
        sb.AppendLine($"{nameof(newParams.DamageRateToPlayer)} || {ogParams.DamageRateToPlayer} || {newParams.DamageRateToPlayer}");
        sb.AppendLine($"{nameof(newParams.ExpRate)} || {ogParams.ExpRate} || {newParams.ExpRate}");
        sb.AppendLine($"{nameof(newParams.DamageRateToEnemyWeak)} || {ogParams.DamageRateToEnemyWeak} || {newParams.DamageRateToEnemyWeak}");
        sb.AppendLine($"{nameof(newParams.DamageRateToPlayerWeak)} || {ogParams.DamageRateToPlayerWeak} || {newParams.DamageRateToPlayerWeak}");
        sb.AppendLine($"{nameof(newParams.DamageRateToEnemyCritical)} || {ogParams.DamageRateToEnemyCritical} || {newParams.DamageRateToEnemyCritical}");
        sb.AppendLine($"{nameof(newParams.DamageRateToPlayerCritical)} || {ogParams.DamageRateToPlayerCritical} || {newParams.DamageRateToPlayerCritical}");
        sb.AppendLine($"{nameof(newParams.MoneyRateToMaterials)} || {ogParams.MoneyRateToMaterials} || {newParams.MoneyRateToMaterials}");
        sb.AppendLine($"{nameof(newParams.BadStatusHitRateFromEnemy)} || {ogParams.BadStatusHitRateFromEnemy} || {newParams.BadStatusHitRateFromEnemy}");
        sb.AppendLine($"{nameof(newParams.BadStatusHitRateFromPlayer)} || {ogParams.BadStatusHitRateFromPlayer} || {newParams.BadStatusHitRateFromPlayer}");
        Log.Debug($"\n{diffName}\n{sb}");
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct FBtlCalcParam
{
    public float DamageRateToEnemy;
    public float DamageRateToPlayer;
    public float ExpRate;
    public float DamageRateToEnemyWeak;
    public float DamageRateToPlayerWeak;
    public float DamageRateToEnemyCritical;
    public float DamageRateToPlayerCritical;
    public float MoneyRateToMaterials;
    public float BadStatusHitRateFromEnemy;
    public float BadStatusHitRateFromPlayer;
}