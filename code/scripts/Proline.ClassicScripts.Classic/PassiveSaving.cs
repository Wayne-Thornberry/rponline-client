using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.Engine.Parts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class PassiveSaving
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            var nextSaveTime = DateTime.UtcNow.AddMinutes(1);
            while (!token.IsCancellationRequested)
            {
                if (DateTime.UtcNow > nextSaveTime)
                {
                    var id = "PlayerInfo";
                    if (EngineAPI.DoesDataFileExist(id))
                    {
                        EngineAPI.SelectDataFile(id);
                        EngineAPI.SetDataFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                    }
                    EngineAPI.StartNewScript("SaveNow");
                    while (EngineAPI.GetInstanceCountOfScript("SaveNow") > 0)
                    {
                        await BaseScript.Delay(1);
                    }
                    nextSaveTime = DateTime.UtcNow.AddMinutes(1);
                }
                await BaseScript.Delay(0);
            }

        }
    }
}
