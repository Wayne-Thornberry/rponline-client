using Newtonsoft.Json;
using Proline.ClassicOnline.CDebugActions;
using Proline.ClassicOnline.CScriptObjs.Data;
using Proline.ClassicOnline.CScriptObjs.Internal;
using Proline.Resource.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CScriptObjs.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            var data2 = ResourceFile.Load("data/brain/script_objs.json");
            var api = new CDebugActionsAPI();
            api.LogDebug(data2);
            var objs = JsonConvert.DeserializeObject<ScriptObjectData[]>(data2.Load());
            var sm = ScriptObjectManager.GetInstance();

            foreach (var item in objs)
            {
                var hash = string.IsNullOrEmpty(item.ModelHash) ? item.ModelName : CitizenFX.Core.Native.API.GetHashKey(item.ModelHash);
                if (!sm.ContainsKey(hash))
                    sm.Add(hash, new List<ScriptObjectData>());
                sm.Get(hash).Add(item);
            }
        }
    }
}
