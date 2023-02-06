using CitizenFX.Core;
using RPOnline.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts
{
    public class ScriptTemplate
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("ScriptTemplate") > 1)
                return;


            while (!token.IsCancellationRequested)
            {

                await EngineAPI.Delay(0);
            }
        }
    }
}
