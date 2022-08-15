using Proline.ClassicOnline.CGameLogic;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class LoadDefaultStats
    {
        public async Task Execute(object[] args, CancellationToken token)
        {
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            stat.SetValue(default);
            stat2.SetValue(default);


        }
    }
}
