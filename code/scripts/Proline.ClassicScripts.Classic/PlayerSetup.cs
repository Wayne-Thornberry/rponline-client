using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.CGameLogic;
using Proline.ClassicOnline.CGameLogic.Data;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class PlayerSetup
    {
        public async Task Execute(object[] args, CancellationToken token)
        {
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            var list = new OutfitComponent[12];
            for (int i = 0; i < list.Length; i++)
            {
                var component = list[i];
                component.ComponentIndex = API.GetPedDrawableVariation(Game.PlayerPed.Handle, i);
                component.ComponentPallet = API.GetPedPaletteVariation(Game.PlayerPed.Handle, i);
                component.ComponentTexture = API.GetPedTextureVariation(Game.PlayerPed.Handle, i);

            }

            var id = "PlayerInfo";
            if (!EngineAPI.DoesDataFileExist(id))
            {
                EngineAPI.CreateDataFile();
                EngineAPI.AddDataFileValue("PlayerHealth", Game.PlayerPed.Health);
                EngineAPI.AddDataFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                EngineAPI.AddDataFileValue("BankBalance", 0);
                EngineAPI.AddDataFileValue("WalletBalance", 0);
                EngineAPI.SaveDataFile(id);
                EngineAPI.LogDebug(id + " Created and saved");
                EngineAPI.SetCharacterMaxWalletBalance(1000);
            }



            //ClassicOnline.CDataStream.API.DoesDataFileExist("PlayerVehicle");
            //if (ClassicOnline.CDataStream.API.DoesDataFileValueExist("VehicleHash"))
            //{
            //    var pv = (VehicleHash)ClassicOnline.CDataStream.API.GetDataFileValue<uint>("VehicleHash");
            //    var position = ClassicOnline.CDataStream.API.GetDataFileValue<Vector3>("VehiclePosition");
            //    var vehicle = await World.CreateVehicle(new Model(pv), position);
            //    vehicle.PlaceOnNextStreet();
            //    vehicle.IsPersistent = true;
            //    if (vehicle.AttachedBlips.Length == 0)
            //        vehicle.AttachBlip();
            //    //blip.IsFlashing = true;
            //}

            id = "PlayerOutfit";
            if (!EngineAPI.DoesDataFileExist(id))
            {
                EngineAPI.CreateDataFile();
                var outfit = new CharacterOutfit();
                outfit.Components = list;
                var json = JsonConvert.SerializeObject(outfit);
                EngineAPI.AddDataFileValue("CharacterOutfit", json);
                EngineAPI.SaveDataFile(id);
                EngineAPI.LogDebug(id + " Created and saved");
            }

            id = "PlayerStats";
            if (!EngineAPI.DoesDataFileExist(id))
            {
                EngineAPI.CreateDataFile();
                EngineAPI.AddDataFileValue("MP0_WALLET_BALANCE", stat.GetValue());
                EngineAPI.AddDataFileValue("BANK_BALANCE", stat2.GetValue());
                EngineAPI.SaveDataFile(id);
                EngineAPI.LogDebug(id + " Created and saved");
            }

        }
    }
}
