using CGameLogic.Data;
using CitizenFX.Core;
using CScriptObjs;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static int[] GetEntityHandlesByTypes(EntityType type)
        {
            var api = new CCScriptObjsAPI();
            return api.GetEntityHandlesByTypes(type);
        }

        public static Entity GetNeariestEntity(EntityType type)
        {
            var api = new CCScriptObjsAPI();
            return api.GetNeariestEntity(type);
        }
    }
}
