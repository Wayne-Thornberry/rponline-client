using CitizenFX.Core;
using Proline.ClassicOnline.CScriptObjs.Tasks;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CScriptObjs.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            var gc = new ProcessScriptObjs();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(1000);
            }
        }
    }
}
