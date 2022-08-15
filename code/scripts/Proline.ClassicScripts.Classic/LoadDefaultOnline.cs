using CitizenFX.Core;
using Proline.ClassicOnline.CGameLogic.Data;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class LoadDefaultOnline
    {
        public async Task Execute(object[] args, CancellationToken token)
        {
            await Game.Player.ChangeModel(new Model(1885233650));
            if (!EngineAPI.HasSaveLoaded())
            { 
                EngineAPI.StartNewScript("LoadDefaultStats");
                while (EngineAPI.GetInstanceCountOfScript("LoadDefaultStats") > 0)
                {
                    await BaseScript.Delay(1);
                }
                EngineAPI.SetCharacter(EngineAPI.CreateCharacter());
            }

            EngineAPI.SetPedOutfit("mp_m_defaultoutfit", Game.PlayerPed.Handle);
        }


    }
}
