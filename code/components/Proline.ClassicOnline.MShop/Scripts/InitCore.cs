using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.CDebugActions;
using Proline.ClassicOnline.CShopCatalogue.Internal;

using Proline.Resource.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CShopCatalogue.Scripts
{
    internal class InitCore
    {
        public async Task Execute()
        {
            var data = ResourceFile.Load("data/catalogue/catalogue-vehicles.json");
            var api = new CDebugActionsAPI();
            api.LogDebug(data);
            CatalougeManager.GetInstance().Register("VehicleCatalouge", JsonConvert.DeserializeObject<VehicleCatalouge>(data.Load()));


          
        }
    }
}
