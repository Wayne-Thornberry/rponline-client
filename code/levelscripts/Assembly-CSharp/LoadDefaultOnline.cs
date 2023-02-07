using CitizenFX.Core; 
using RPOnline.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts
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
                    await EngineAPI.Delay(1);
                }
                EngineAPI.SetCharacter(EngineAPI.CreateCharacter());
            }

            EngineAPI.SetPedOutfit("mp_m_defaultoutfit", Game.PlayerPed.Handle);
        }


    }
}
