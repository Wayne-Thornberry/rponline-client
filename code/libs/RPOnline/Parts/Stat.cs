using CGameLogic;
using CitizenFX.Core; 

namespace RPOnline.Parts
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
