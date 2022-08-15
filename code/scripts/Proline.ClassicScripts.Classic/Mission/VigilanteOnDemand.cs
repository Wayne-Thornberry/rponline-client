using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.CGameLogic.Data;
using Proline.ClassicOnline.Engine.Parts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Mission
{
    public class VigilanteOnDemand
    {
        private Vehicle _policeVehicle;
        private Vehicle _target;
        private int _payout;
        private float _closestDistance;
        private Ped[] _targetPeds;
        private List<Blip> _blips;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("VigilanteOnDemand") > 1)
                return;
            if (!EngineAPI.BeginMission())
                return;
            _blips = new List<Blip>();
            _closestDistance = 99999f;

            //_truckSpawnLoc = new Vector3(829.9249f, -2950.439f, 4.902536f);
            //_trailerSpawnLoc = new Vector3(865.3315f, -2986.426f, 4.900764f);
            _policeVehicle = (Vehicle)Entity.FromHandle(int.Parse(args[0].ToString()));
            EngineAPI.TrackPoolObjectForMission(_policeVehicle);


            var handles = EngineAPI.GetEntityHandlesByTypes(EntityType.VEHICLE);

            foreach (var item in handles)
            {
                var entity = (Vehicle)Entity.FromHandle(item);
                var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                if (distance < _closestDistance && IsValidModel(entity.Model) && entity != _policeVehicle && entity.Driver != null)
                {
                    if (entity.Driver.IsDead) continue;
                    EngineAPI.LogDebug("Found a vehicle");
                    _target = entity;
                    _closestDistance = distance;
                }
            }

            if (_target == null)
                return;


            EngineAPI.TrackPoolObjectForMission(_target);

            Setup();

            while (!token.IsCancellationRequested)
            {
                if (!DoCore())
                {
                    break;
                }
                await BaseScript.Delay(0);
            }

            Teardown();
            EngineAPI.EndMission();

        }

        private void Teardown()
        {
            if (IsAllTargetsDead())
            {
                EngineAPI.AddValueToBankBalance(_payout);
            }

            DeleteAllBlips(_policeVehicle);
            foreach (var item in _targetPeds)
            {
                DeleteAllBlips(item);

                if (item.LastVehicle != null)
                    DeleteAllBlips(item.LastVehicle);
                if (item.CurrentVehicle != null)
                    DeleteAllBlips(item.CurrentVehicle);
            }

            foreach (var item in _blips)
            {
                item.Delete();
            }
        }

        private void Setup()
        {
            _blips.Add(_policeVehicle.AttachBlip());
            var targets = new List<Ped>(_target.Passengers);
            targets.Add(_target.Driver);
            _targetPeds = targets.ToArray();
            _payout = 1000 * _targetPeds.Length;




            Screen.ShowNotification("Vigilante Started");
            Game.PlayerPed.RelationshipGroup = new RelationshipGroup(1);

            foreach (var item in _targetPeds)
            {
                item.IsPersistent = true;
                _blips.Add(item.AttachBlip());
                item.Weapons.Give(WeaponHash.AdvancedRifle, 500, true, true);
                item.Weapons.Give(WeaponHash.Pistol, 500, true, true);
                item.RelationshipGroup = new RelationshipGroup(0);
                item.RelationshipGroup.SetRelationshipBetweenGroups(Game.PlayerPed.RelationshipGroup, Relationship.Hate, true);
                item.Task.FightAgainstHatedTargets(15f);
                EngineAPI.TrackPoolObjectForMission(item);
            }
        }

        private bool DoCore()
        {
            Game.Player.WantedLevel = 0;
            if (Game.PlayerPed.IsDead || _policeVehicle.IsDead || IsAllTargetsDead())
                return false;

            foreach (var item in _targetPeds)
            {
                ManagePedBlip(item);
            }

            _policeVehicle.AttachedBlip.Alpha = 0;
            ManagePlayerBlip();
            return true;
        }

        private void ManagePlayerBlip()
        {
            _policeVehicle.AttachedBlip.Alpha = 255;
            if (Game.PlayerPed.IsInVehicle())
            {
                var vehicle = Game.PlayerPed.CurrentVehicle;
                if (vehicle == _policeVehicle)
                {
                    _policeVehicle.AttachedBlip.Alpha = 0;
                }
            }
        }

        private void ManagePedBlip(Ped item)
        {
            CurrentVehicleCheck(item);
            LastVehicleCheck(item);
        }

        private void LastVehicleCheck(Ped item)
        {
            if (item.LastVehicle != null)
            {
                var vehicle = item.LastVehicle;
                DeleteAllBlips(vehicle);
                if (item.AttachedBlip != null)
                {
                    item.AttachedBlip.Alpha = 255;
                }
            }
        }

        private void CurrentVehicleCheck(Ped ped)
        {
            if (ped == null)
            {
                EngineAPI.LogDebug("PED IS NULL WTF?????");
            }
            if (ped.IsInVehicle())
            {
                if (ped.CurrentVehicle != null)
                {
                    if (ped.CurrentVehicle.AttachedBlip == null)
                    {
                        var blip = ped.CurrentVehicle.AttachBlip();
                        blip.Alpha = 255;
                        _blips.Add(blip);
                    }

                    if (ped.AttachedBlip != null)
                    {
                        ped.AttachedBlip.Alpha = 0;
                    }
                }
            }
        }

        private void DeleteAllBlips(Entity item)
        {
            if (item == null) return;
            var blips = item.AttachedBlips;
            foreach (var blip in blips)
            {
                blip.Delete();
            }
        }

        private bool IsAllTargetsDead()
        {
            foreach (var item in _targetPeds)
            {
                if (!item.IsDead) return false;
            }
            return true;
        }

        private bool IsValidModel(Model model)
        {
            return model.IsCar;
        }
    }
}
