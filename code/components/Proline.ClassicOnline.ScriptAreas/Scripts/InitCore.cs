using Newtonsoft.Json;
using Proline.ClassicOnline.CDebugActions;
using Proline.ClassicOnline.CScriptAreas.Data;
using Proline.ClassicOnline.CScriptAreas.Entity;
using Proline.Resource.IO;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CScriptAreas.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            var instance = ScriptPositionManager.GetInstance();
            var data = ResourceFile.Load("data/brain/script_pos.json");
            var api = new CDebugActionsAPI();
            api.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data.Load());
            instance.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);
            PosBlacklist.Create();
        }
    }
}
