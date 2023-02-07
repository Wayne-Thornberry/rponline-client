using Newtonsoft.Json;
using Proline.Resource.Framework;
using Proline.Resource.IO;
using RPOnline.Parts;

namespace RPOnlineGame.Commands
{
    public class BuyVehicleCommand : ResourceCommand
    {
        public BuyVehicleCommand() : base("BuyVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length > 0)
            {
                var vehicle = args[0].ToString(); 
                EngineAPI.BuyVehicle(vehicle);
            }

        }
    }
}
