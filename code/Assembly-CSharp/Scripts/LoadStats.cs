
using Proline.ClassicOnline.Engine.Parts;
using Proline.OnlineEngine.Core;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts
{
    public class LoadStats
    {
        public async Task Execute(object[] args, CancellationToken token)
        {
            if (EngineAPI.HasCharacter())
            {
                var stats = EngineAPI.GetChracterStats();
                if (stats == null)
                    return;
                var walletBalanceStat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                var bankBalanceStat = MPStat.GetStat<long>("BANK_BALANCE");


                var walletBalance = stats["WALLET_BALANCE"];
                var bankBalance = stats["BANK_BALANCE"];

                EngineAPI.LogDebug(walletBalance);
                EngineAPI.LogDebug(bankBalance);

                walletBalanceStat.SetValue(EngineAPI.GetCharacterWalletBalance());
                bankBalanceStat.SetValue(EngineAPI.GetCharacterBankBalance());
            }
        }
    }
}
