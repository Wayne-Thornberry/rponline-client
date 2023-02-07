using CGameLogic;
using CitizenFX.Core;

using Proline.Resource.Framework;

namespace RPOnlineGame.Commands
{
    public class BuyRandomOutfitCommand : ResourceCommand
    {
        public BuyRandomOutfitCommand() : base("BuyRandomOutfit")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var EngineAPI = new CGameLogicAPI();
            if (EngineAPI.GetCharacterBankBalance() > 250)
            {
                Game.Player.Character.Style.RandomizeOutfit();
                Game.Player.Character.Style.RandomizeProps();
                EngineAPI.SetCharacterBankBalance(250);
            }
        }
    }
}
