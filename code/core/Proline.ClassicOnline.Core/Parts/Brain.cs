using CitizenFX.Core;
using Proline.ClassicOnline.CGameLogic.Data;
using Proline.ClassicOnline.CScriptObjs;

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
