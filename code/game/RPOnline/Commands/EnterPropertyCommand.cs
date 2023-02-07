using Proline.Resource.Framework;
using RPOnline.Parts;

namespace RPOnlineGame.Commands
{
    public class EnterProperty : ResourceCommand
    {
        public EnterProperty() : base("EnterProperty")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length == 3)
            { 
                EngineAPI.EnterProperty(args[0].ToString(), args[1].ToString(), args[2].ToString());
            }
        }
    }
}
