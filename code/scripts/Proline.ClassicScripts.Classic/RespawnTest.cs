using CitizenFX.Core;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    internal class RespawnTest
    {
        private Vector3 test;
        private float tes2;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("RespawnTest") > 1)
                return;
            //API.SpawnpointsStartSearch();



            //API.DisableAutomaticRespawn(false);
            //API.NetworkGetRespawnResult(2, ref test, ref tes2);
            //var x = API.NetworkGetRespawnResultFlags(2);
            //CDebugActions.EngineAPI.LogDebug(x + "");
            //API.SetNextRespawnToCustom();
            //API.SetCustomRespawnPosition(test.X, test.Y, test.Z, 0);
            //API.NetworkRespawnCoords(Game.PlayerPed.Handle, test.X, test.Y, test.Z, false, false);
            //API.NetworkStartRespawnSearchForPlayer(Game.PlayerPed.Handle, test.X, test.Y, test.Z, 5f, 1f,1f,1f,x); 
        }
    }
}
