using CitizenFX.Core;
using Proline.ClassicOnline.CGameLogic;
using Proline.ClassicOnline.CGameLogic.Data;
using Proline.ClassicOnline.CScriptObjs;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    { 
        public static void SetStatLong(string statName, long value)
        { 
            var api = new CGameLogicAPI();
            api.SetStatLong(statName, value);
        }
    }
}
