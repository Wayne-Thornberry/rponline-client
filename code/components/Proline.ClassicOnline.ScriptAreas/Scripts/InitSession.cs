using CitizenFX.Core;
using Proline.ClassicOnline.CScriptAreas.Tasks;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CScriptAreas.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            var gc = new ProcessScriptAreas();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(1000);
            }
        }
    }
}
