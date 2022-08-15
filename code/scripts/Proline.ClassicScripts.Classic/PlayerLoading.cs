using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.CGameLogic.Data;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    internal class PlayerLoading
    {
        public async Task Execute(object[] args, CancellationToken token)
        {

            if (!EngineAPI.IsSaveInProgress())
            {
                // attempt to get the player id
                // fish for the save files from the player id
                EngineAPI.StartNewScript("LoadDefaultOnline");
                while (EngineAPI.GetInstanceCountOfScript("LoadDefaultOnline") > 0)
                {
                    await BaseScript.Delay(1);
                }
                await EngineAPI.PullSaveFromCloud(); // Sends a load request to the server
                if (EngineAPI.HasSaveLoaded())
                {
                    int character = EngineAPI.CreateCharacter();
                    EngineAPI.SetCharacter(character);

                    if (EngineAPI.DoesDataFileExist("PlayerInfo"))
                    {
                        EngineAPI.SelectDataFile("PlayerInfo");
                        Game.PlayerPed.Health = EngineAPI.GetDataFileValue<int>("PlayerHealth");
                        Game.PlayerPed.Position = EngineAPI.GetDataFileValue<Vector3>("PlayerPosition");
                        EngineAPI.SetCharacterBankBalance(EngineAPI.GetDataFileValue<long>("BankBalance"));
                        EngineAPI.SetCharacterWalletBalance(EngineAPI.GetDataFileValue<long>("WalletBalance"));
                    }
                    else
                    {
                        EngineAPI.LogDebug("PlayerStats does not exist");
                    }
                    EngineAPI.LogDebug(EngineAPI.GetSelectedDataFile());

                    if (EngineAPI.DoesDataFileExist("PlayerStats"))
                    {
                        EngineAPI.SelectDataFile("PlayerStats");
                        var x = EngineAPI.GetDataFileValue<int>("MP0_WALLET_BALANCE");
                        var y = EngineAPI.GetDataFileValue<int>("BANK_BALANCE");

                        EngineAPI.SetStatLong("WALLET_BALANCE", x);
                        EngineAPI.SetStatLong("BANK_BALANCE", x);
                    }
                    else
                    {
                        EngineAPI.LogDebug("PlayerStats does not exist");
                    }
                    EngineAPI.LogDebug(EngineAPI.GetSelectedDataFile());

                    if (EngineAPI.DoesDataFileExist("CharacterLooks"))
                    {
                        EngineAPI.SelectDataFile("CharacterLooks");
                        var mother = EngineAPI.GetDataFileValue<int>("Mother");
                        EngineAPI.LogDebug("DASDSAUIODHBAUIDJABNDJASDDANSJKDBNSADJK:::: " + mother);
                        var father = EngineAPI.GetDataFileValue<int>("Father");
                        var resemblence = EngineAPI.GetDataFileValue<float>("Resemblance");
                        var skintone = EngineAPI.GetDataFileValue<float>("SkinTone");

                        EngineAPI.SetPedLooks(Game.PlayerPed.Handle, mother,
                            father,
                             resemblence * 0.1f,
                           skintone * 0.1f);
                    }
                    else
                    {
                        EngineAPI.LogDebug("CharacterLooks does not exist");
                    }
                    EngineAPI.LogDebug(EngineAPI.GetSelectedDataFile());

                    if (EngineAPI.DoesDataFileExist("PlayerOutfit"))
                    {
                        EngineAPI.SelectDataFile("PlayerOutfit");
                        var outfit = EngineAPI.GetDataFileValue<CharacterOutfit>("CharacterOutfit");
                        var components = outfit.Components;
                        for (int i = 0; i < components.Length; i++)
                        {
                            var component = components[i];
                            API.SetPedComponentVariation(Game.PlayerPed.Handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);

                        }
                    }
                    else
                    {
                        EngineAPI.LogDebug("PlayerOutfit does not exist");
                    }

                    EngineAPI.LogDebug(EngineAPI.GetSelectedDataFile());

                    if (EngineAPI.DoesDataFileExist("PlayerVehicle"))
                    {
                        EngineAPI.SelectDataFile("PlayerVehicle");
                        if (EngineAPI.DoesDataFileValueExist("VehicleHash"))
                        {
                            EngineAPI.LogDebug("vehicleASD:::: ");
                            var pv = (VehicleHash)(uint)EngineAPI.GetDataFileValue<int>("VehicleHash");
                            EngineAPI.LogDebug("vehicleASD:::: " + pv);
                            var position = EngineAPI.GetDataFileValue<Vector3>("VehiclePosition");
                            var vehicle = await World.CreateVehicle(new Model(pv), Game.PlayerPed.Position);
                            vehicle.PlaceOnNextStreet();
                            vehicle.IsPersistent = true;
                            if (vehicle.AttachedBlips.Length == 0)
                                vehicle.AttachBlip();
                            EngineAPI.SetCharacterPersonalVehicle(vehicle.Handle);
                        }
                    }
                    else
                    {
                        EngineAPI.LogDebug("PlayerVehicle does not exist");
                    }

                    EngineAPI.LogDebug(EngineAPI.GetSelectedDataFile());
                    if (EngineAPI.DoesDataFileExist("PlayerWeapon"))
                    {
                        EngineAPI.SelectDataFile("PlayerWeapon");
                        if (EngineAPI.DoesDataFileValueExist("WeaponHash"))
                        {
                            var hash = (WeaponHash)EngineAPI.GetDataFileValue<uint>("WeaponHash");
                            var ammo = EngineAPI.GetDataFileValue<int>("WeaponAmmo");
                            Game.PlayerPed.Weapons.Give(hash, ammo, true, true);
                        }
                    }
                    else
                    {
                        EngineAPI.LogDebug("PlayerWeapon does not exist");
                    }
                    EngineAPI.LogDebug(EngineAPI.GetSelectedDataFile());

                    EngineAPI.StartNewScript("LoadStats");

                }
                else
                {
                    EngineAPI.LogDebug("No save file to load from, attempting to create a save...");
                    /// If the player doesnt have basic info, that means the player does not have a save
                    if (!EngineAPI.DoesDataFileExist("PlayerInfo"))
                    {
                        EngineAPI.StartNewScript("PlayerSetup");
                        while (EngineAPI.GetInstanceCountOfScript("PlayerSetup") > 0)
                        {
                            await BaseScript.Delay(1);
                        }
                        EngineAPI.StartNewScript("SaveNow");
                        while (EngineAPI.GetInstanceCountOfScript("SaveNow") > 0)
                        {
                            await BaseScript.Delay(1);
                        }
                    }
                    EngineAPI.StartNewScript("PlayerLoading");
                }

            }

        }

    }
}
