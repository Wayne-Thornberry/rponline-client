using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.CDataStream;
using Proline.ClassicOnline.CDebugActions;
using Proline.ClassicOnline.CGameLogic;
using Proline.ClassicOnline.CShopCatalogue.Internal;
using System;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CShopCatalogue
{
    public class CShopCatalogueAPI : ICShopCatalogueAPI
    {
        public void BuyVehicle(string vehicleName)
        {
            var api = new CDebugActionsAPI();
            var gameApi = new CGameLogicAPI();
            try
            {
                if (gameApi.HasCharacter())
                {
                    var manager = CatalougeManager.GetInstance();
                    var catalouge = manager.GetCatalouge("VehicleCatalouge");
                    if (catalouge == null)
                        throw new Exception("Catalouge not found");
                    var vci = (VehicleCatalougeItem)catalouge.GetItem(vehicleName);

                    if (vci == null)
                        throw new Exception("Catalouge Item not found");
                    if (gameApi.HasBankBalance(vci.Price))
                    {
                        var currentVehicle = gameApi.GetPersonalVehicle();
                        if (currentVehicle != null)
                        {
                            foreach (var item in currentVehicle.AttachedBlips)
                            {
                                item.Delete();
                            }
                            currentVehicle.IsPersistent = false;
                            currentVehicle.Delete();
                        }


                        var task = World.CreateVehicle(new Model(vci.Model), World.GetNextPositionOnStreet(Game.PlayerPed.Position));
                        task.ContinueWith(e =>
                        {
                            var createdVehicle = e.Result;
                            gameApi.SetCharacterPersonalVehicle(createdVehicle.Handle);
                            var dataAPI = new CDataStreamAPI();
                            var id = "PlayerVehicle";
                            dataAPI.CreateDataFile();
                            dataAPI.AddDataFileValue("VehicleHash", createdVehicle.Model.Hash);
                            dataAPI.AddDataFileValue("VehiclePosition", JsonConvert.SerializeObject(createdVehicle.Position));
                            createdVehicle.IsPersistent = true;
                            if (createdVehicle.AttachedBlips.Length == 0)
                                createdVehicle.AttachBlip();
                            dataAPI.SaveDataFile(id);
                            gameApi.SubtractValueFromBankBalance(vci.Price);
                        });

                    }
                }

            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }
    }
}
