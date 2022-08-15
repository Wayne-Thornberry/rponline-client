using Proline.ClassicOnline.CGameLogic;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
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


                var walletBalance = stats.GetStat("WALLET_BALANCE");
                var bankBalance = stats.GetStat("BANK_BALANCE");

                EngineAPI.LogDebug(walletBalance);
                EngineAPI.LogDebug(bankBalance);

                walletBalanceStat.SetValue(EngineAPI.GetCharacterWalletBalance());
                bankBalanceStat.SetValue(EngineAPI.GetCharacterBankBalance());
            }
        }
    }
}
