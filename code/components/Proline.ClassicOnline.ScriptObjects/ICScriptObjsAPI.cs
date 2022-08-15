using Proline.ClassicOnline.CGameLogic.Data;

namespace Proline.ClassicOnline.CScriptObjs
{
    public interface ICScriptObjsAPI
    {
        int[] GetEntityHandlesByTypes(EntityType type);
        CitizenFX.Core.Entity GetNeariestEntity(EntityType type);
    }
}