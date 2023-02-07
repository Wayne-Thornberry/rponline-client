using CitizenFX.Core;


using Proline.Resource.Framework;
using RPOnline.Parts;

namespace RPOnlineGame.Commands
{
    public class SetPlayerPedLooksCommand : ResourceCommand
    {
        public SetPlayerPedLooksCommand() : base("SetPlayerPedLooks")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length == 3)
            { 
                var Father = int.Parse(args[0].ToString());
                var Mother = int.Parse(args[1].ToString());
                var Resemblence = float.Parse(args[2].ToString());
                EngineAPI.SetPedLooks(Game.PlayerPed.Handle, Mother, Father, Resemblence, 0f);
            }

        }
    }
}
