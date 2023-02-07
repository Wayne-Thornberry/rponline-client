using CitizenFX.Core;

using Proline.Resource.Framework;
using RPOnline.Parts;

namespace RPOnlineGame.Commands
{
    public class TpToNeariestEntrance : ResourceCommand
    {
        public TpToNeariestEntrance() : base("TpToNeariestEntrance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        { 
            var entrance = EngineAPI.GetBuildingPosition(EngineAPI.GetNearestBuilding());
            Game.PlayerPed.Position = entrance;
        }
    }
}
