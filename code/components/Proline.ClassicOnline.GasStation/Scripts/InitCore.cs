using Newtonsoft.Json;
using Proline.ClassicOnline.CDebugActions;
using Proline.ClassicOnline.CWorldObjects.Data.Ownership;
using Proline.ClassicOnline.CWorldObjects.Internal;
using Proline.Resource.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CWorldObjects.Scripts
{
    public class InitCore
    {


        public async Task Execute()
        {
            var api = new CDebugActionsAPI();
            try
            {
                var apartmentData = ResourceFile.Load("data/world/apt_properties.json");
                var pm = PropertyManager.GetInstance();
                var apartmentInteriors = JsonConvert.DeserializeObject<List<PropertyMetadata>>(apartmentData.Load());
                foreach (var item in apartmentInteriors)
                {
                    pm.Register(item.Id, item);
                }
            }
            catch (System.Exception e)
            {
                api.LogDebug(e);
            }

        }


    }
}
