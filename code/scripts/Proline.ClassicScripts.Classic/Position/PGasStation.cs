using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Position
{
    public class PGasStation
    {
        private bool _isFilling;
        private float _fillAmount;
        private float _fillPerTick;

        public async Task Execute(object[] args, CancellationToken token)
        {
            var position = (Vector3)args[0];
            _fillPerTick = 0.3f;
            while (!token.IsCancellationRequested)
            {
                if (World.GetDistance(position, Game.PlayerPed.Position) > 10f)
                {
                    break;
                }

                if (Game.PlayerPed.IsInVehicle())
                {
                    var vehicle = Game.PlayerPed.CurrentVehicle;
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to refil the vehicle");
                    if (Game.IsControlJustPressed(0, Control.Context))
                    {
                        EngineAPI.LogDebug(vehicle.FuelLevel);
                    }
                    else if (Game.IsControlPressed(0, Control.Context))
                    {
                        if (vehicle.FuelLevel < 100f)
                        {
                            vehicle.FuelLevel += _fillPerTick * Game.LastFrameTime;
                            _fillAmount += _fillPerTick * Game.LastFrameTime;
                            _isFilling = true;
                        }
                    }
                    else if (Game.IsControlJustReleased(0, Control.Context) && _isFilling)
                    {
                        EngineAPI.LogDebug(vehicle.FuelLevel);
                        EngineAPI.LogDebug(_fillAmount);
                        var cost = (int)(_fillAmount * 100.00f);
                        EngineAPI.LogDebug(cost);
                        EngineAPI.SetCharacterBankBalance(EngineAPI.GetCharacterBankBalance() - cost);
                        _fillAmount = 0f;
                        _isFilling = false;
                    }
                }

                await BaseScript.Delay(0);
            }
        }
    }
}
