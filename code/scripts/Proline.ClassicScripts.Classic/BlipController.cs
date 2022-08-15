using CitizenFX.Core;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class BlipController
    {

        public async Task Execute(object[] args, CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {
                if (EngineAPI.HasCharacter())
                {
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        var currentVehicle = Game.PlayerPed.CurrentVehicle;
                        var personalVehicle = EngineAPI.GetPersonalVehicle();
                        if (personalVehicle != null)
                        {
                            if (currentVehicle == personalVehicle)
                            {
                                foreach (var blip in currentVehicle.AttachedBlips)
                                {
                                    blip.Alpha = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        var personalVehicle = EngineAPI.GetPersonalVehicle();
                        if (personalVehicle != null)
                        {
                            foreach (var blip in personalVehicle.AttachedBlips)
                            {
                                blip.Alpha = 255;
                            }
                        }
                    }
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
