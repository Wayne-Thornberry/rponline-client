using CDataStream;
using CGameLogic;
using CitizenFX.Core;
using Newtonsoft.Json;


using Proline.Resource.Framework;
using System;

namespace RPOnlineGame.Commands
{
    public class BuyRandomVehicleCommand : ResourceCommand
    {
        public BuyRandomVehicleCommand() : base("BuyRandomVehicle")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {

            var EngineAPI = new CGameLogicAPI();
            if (EngineAPI.GetCharacterBankBalance() > 250)
            {
                if (EngineAPI.HasCharacter())
                {
                    if (EngineAPI.GetPersonalVehicle() != null)
                    {
                        EngineAPI.DeletePersonalVehicle();
                    }


                    EngineAPI.SetCharacterBankBalance(250);
                    Array values = Enum.GetValues(typeof(VehicleHash));
                    Random random = new Random();
                    VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                    var task = World.CreateVehicle(new Model(randomBar), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                    task.ContinueWith(e =>
                    {
                        var vehicle = e.Result;
                        EngineAPI.SetCharacterPersonalVehicle(vehicle.Handle);

                        var id = "PlayerVehicle";
                        var dataAPI = new CDataStreamAPI();
                        dataAPI.CreateDataFile();
                        dataAPI.AddDataFileValue("VehicleHash", vehicle.Model.Hash);
                        dataAPI.AddDataFileValue("VehiclePosition", JsonConvert.SerializeObject(vehicle.Position));
                        vehicle.IsPersistent = true;
                        if (vehicle.AttachedBlips.Length == 0)
                            vehicle.AttachBlip();
                        dataAPI.SaveDataFile(id);

                    });
                }


            }
        }
    }
}
