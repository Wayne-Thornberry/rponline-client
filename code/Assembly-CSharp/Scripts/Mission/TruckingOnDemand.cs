﻿
using CitizenFX.Core;

using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts.Mission
{
    public class TruckingOnDemand
    {
        private Vector3 _truckSpawnLoc;
        private Vector3 _trailerSpawnLoc;
        private Vehicle _truck;
        private Vehicle _trailer;
        private Vector3 _deliveryLoc;
        private int _payout;
        private Blip _deliveryBlip;
        private float _closestDistance;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("TruckingOnDemand") > 1)
                return;
            if (!EngineAPI.BeginMission())
                return;

            _closestDistance = 99999f;

            _truckSpawnLoc = new Vector3(829.9249f, -2950.439f, 4.902536f);
            _trailerSpawnLoc = new Vector3(865.3315f, -2986.426f, 4.900764f);
            _truck = (Vehicle)Entity.FromHandle(int.Parse(args[0].ToString()));


            var handles = EngineAPI.GetEntityHandlesByTypes(2);

            foreach (var item in handles)
            {
                var entity = Entity.FromHandle(item);
                var distance = World.GetDistance(entity.Position, Game.PlayerPed.Position);
                if (distance < _closestDistance && IsValidModel(entity.Model))
                {
                    _trailer = (Vehicle)entity;
                    _closestDistance = distance;
                }
            }

            if (_trailer == null)
                return;

            //_trailer = await World.CreateVehicle(VehicleHash.Trailers2, _trailerSpawnLoc, 270);
            _deliveryLoc = new Vector3(-430.9589f, -2713.246f, 5.000218f);
            _payout = (int)(10.0f * World.GetDistance(_trailer.Position, _deliveryLoc));

            _truck.AttachBlip();
            _trailer.AttachBlip();
            _deliveryBlip = World.CreateBlip(_deliveryLoc);


            EngineAPI.TrackPoolObjectForMission(_truck);
            EngineAPI.TrackPoolObjectForMission(_trailer);
            EngineAPI.TrackPoolObjectForMission(_deliveryBlip);

            while (!token.IsCancellationRequested)
            {
                if (_truck.IsDead || _trailer.IsDead)
                    break;

                if (_truck.IsAttachedTo(_trailer))
                {
                    _truck.AttachedBlip.Alpha = 0;
                    if (Game.PlayerPed.CurrentVehicle == _truck)
                    {
                        _truck.AttachedBlip.Alpha = 0;
                        _deliveryBlip.Alpha = 255;
                    }
                }
                else
                {
                    if (World.GetDistance(_trailer.Position, _deliveryLoc) < 10f)
                    {
                        _trailer.Delete();
                        EngineAPI.AddValueToBankBalance(_payout);
                        break;
                    }
                }

                await EngineAPI.Delay(0);
            }
            EngineAPI.EndMission();
        }

        private bool IsValidModel(Model model)
        {
            return model == VehicleHash.Trailers || model == VehicleHash.Trailers2 || model == VehicleHash.Trailers3 || model == VehicleHash.Trailers4;
        }
    }
}
