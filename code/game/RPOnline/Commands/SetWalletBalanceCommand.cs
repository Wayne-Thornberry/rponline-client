using Proline.Resource.Framework;
using RPOnline.Parts;

namespace RPOnlineGame.Commands
{
    public class SetWalletBalanceCommand : ResourceCommand
    {
        public SetWalletBalanceCommand() : base("SetWalletBalance")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        { 
            if (args.Length > 0)
            {
                long.TryParse(args[0].ToString(), out var value);
                EngineAPI.SetCharacterWalletBalance(value);
            }

        }
    }
}
