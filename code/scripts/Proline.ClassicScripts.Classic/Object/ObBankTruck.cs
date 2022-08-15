using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.CGameLogic;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Object
{
    public class ObBankTruck
    {
        private Blip _blip;

        public ObBankTruck()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            if (args.Length > 0)
            {
                var entityHandle = (int)args[0];
                EngineAPI.LogDebug(entityHandle);
                var entity = Entity.FromHandle(entityHandle);
                _blip = entity.AttachBlip();
                _blip.Sprite = BlipSprite.ArmoredTruck;
                var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                EngineAPI.LogDebug(stat.GetValue());
                EngineAPI.LogDebug(stat2.GetValue());
                while (entity.Exists() && !token.IsCancellationRequested)
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recive money");
                    if (Game.IsControlJustPressed(0, Control.Context))
                    {
                        stat.SetValue(stat.GetValue() + 1000);
                        break;
                    }
                    await BaseScript.Delay(0);
                }

                if (_blip != null)
                    _blip.Delete();
            }
        }
    }
}
