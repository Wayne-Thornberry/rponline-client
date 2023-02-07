using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using RPOnline.Parts;
using RPOnlineCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts
{
    public class Freemode
    {
        private Vector3 _missionTruckingLocationStart;
        private Blip _missionTruckingBlip;
        private bool _hasHelpTextDisplayed;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("Freemode") > 1)
                return;

            var x = await EngineAPI.GetVehicleNames();
            EngineAPI.LogDebug(x);


            _missionTruckingLocationStart = new Vector3(798.6685f, -2977.404f, 5.020939f);
            _missionTruckingBlip = World.CreateBlip(_missionTruckingLocationStart);
            _missionTruckingBlip.Sprite = BlipSprite.TowTruck;

            while (!token.IsCancellationRequested)
            {
                if (Game.IsControlJustPressed(0, Control.PushToTalk))
                {
                    API.SetNuiFocus(!API.IsNuiFocused(), !API.IsNuiFocused());

                } 

                if (Game.IsControlJustPressed(0, Control.PhoneUp))
                {
                    var names = await EngineAPI.GetVehicleNames();
                    if(names.Length > 0)
                    {
                        var name = names.Split(',').First();
                        var vehicle = await World.CreateVehicle(new Model(name), Game.PlayerPed.Position);
                        EngineAPI.DeleteVehicle(name);
                    }
                }

                var character = new Character();
                var u = character.BankBalance;


                if (!EngineAPI.GetMissionFlag())
                {
                    if (Game.PlayerPed.CurrentVehicle != null)
                    {
                        var currentVehicle = Game.PlayerPed.CurrentVehicle;
                        if (currentVehicle.Model == VehicleHash.Phantom || currentVehicle.Model == VehicleHash.Hauler)
                        {
                            if (!_hasHelpTextDisplayed)
                            {
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ To Start Trucking");
                                API.PlaySoundFrontend(-1, "INFO", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                                _hasHelpTextDisplayed = true;
                            }
                            if (Game.IsControlJustPressed(0, Control.Context) &&
                                EngineAPI.GetInstanceCountOfScript("TruckingOnDemand") == 0)
                            {
                                EngineAPI.StartNewScript("TruckingOnDemand", currentVehicle.Handle);
                            }
                        }
                        else if (currentVehicle.Model == VehicleHash.Police || currentVehicle.Model == VehicleHash.Police2)
                        {
                            if (!_hasHelpTextDisplayed)
                            {
                                Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ To Start Vigilante");
                                API.PlaySoundFrontend(-1, "INFO", "HUD_FRONTEND_DEFAULT_SOUNDSET", true);
                                _hasHelpTextDisplayed = true;
                            }
                            if (Game.IsControlJustPressed(0, Control.Context) &&
                                EngineAPI.GetInstanceCountOfScript("VigilanteOnDemand") == 0)
                            {
                                EngineAPI.StartNewScript("VigilanteOnDemand", currentVehicle.Handle);
                            }
                        }
                    }
                    else
                    {
                        if (_hasHelpTextDisplayed)
                            _hasHelpTextDisplayed = false;
                    }


                    if (_missionTruckingBlip != null)
                    {
                        World.DrawMarker(MarkerType.VerticalCylinder, _missionTruckingLocationStart, new Vector3(0, 0, 0),
                            new Vector3(0, 0, 0), new Vector3(1, 1, 1), System.Drawing.Color.FromArgb(150, 145, 0, 0));

                        if (World.GetDistance(Game.PlayerPed.Position, _missionTruckingLocationStart) <= 2f && EngineAPI.GetInstanceCountOfScript("Trucking") == 0)
                        {
                            EngineAPI.StartNewScript("Trucking");
                        }
                    }
                }

                await EngineAPI.Delay(0);
            }
        }
    }
}
