using CitizenFX.Core;
using Proline.Resource.Framework;

namespace Proline.ClassicOnline.CGameLogic.Commands
{
    public class KillSelfCommand : ResourceCommand
    {
        public KillSelfCommand() : base("KillSelf")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            Game.PlayerPed.Kill();
        }
    }
}
