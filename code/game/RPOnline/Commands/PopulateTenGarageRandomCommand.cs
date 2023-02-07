using CitizenFX.Core;
using Proline.Resource.Framework;
using RPOnline.Parts;
using System.Threading.Tasks;

namespace RPOnlineGame.Commands
{
    public class PopulateTenGarageRandomCommand : ResourceCommand
    {
        public PopulateTenGarageRandomCommand() : base("PopulateTenGarageRandom")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            DoStuff();
        }

        private static async Task DoStuff()
        {
            for (int i = 0; i < 10; i++)
            {
                //Array values = Enum.GetValues(typeof(VehicleHash));
                //Random random = new Random();
                //VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                var vehicle = await World.CreateVehicle(new Model(VehicleHash.Buffalo3), Game.PlayerPed.Position);
                EngineAPI.PlaceVehicleInGarageSlot("apt_richmaj_he_01", i, vehicle);
            }
        }
    }
}
