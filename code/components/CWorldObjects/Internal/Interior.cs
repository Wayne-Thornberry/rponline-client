using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;

namespace CWorldObjects.Internal
{
    public class Interior : IExistable
    {
        public Interior(int interiorId)
        {
            InteriorId = interiorId;
        }

        public Vector3 Position => GetInteriorPosition(InteriorId);
        public Vector3 Rotation => GetInteriorRotation(InteriorId);
        public int InteriorId { get; set; }
        public int InteriorGroup => GetInteriorGroup(InteriorId);
        public bool IsReady => IsInteriorReady(InteriorId);
        public bool IsPlayerInInterior => IsEntityInInterior(InteriorId, Game.PlayerPed);

        public bool IsActive
        {
            set => SetInteriorActive(InteriorId, value);
        }

        public bool Enable
        {
            get => IsInteriorDisabled(InteriorId);
            set => DisableInterior(InteriorId, !value);
        }

        public void Load()
        {
            LoadInterior(InteriorId);
        }

        public override string ToString()
        {
            return "InteriorId: " + InteriorId +
                   "\nInteriorGroup: " + InteriorGroup +
                   "\nIsReady: " + IsReady +
                   "\nPosition: " + Position +
                   "\nRotation: " + Rotation;
        }

        public bool Exists()
        {
            return IsInteriorReady(InteriorId) && !IsInteriorDisabled(InteriorId);
        }

        #region StaticMethods

        public bool IsEntityInAInterior(Entity entity)
        {
            return GetInteriorIdAtCords(entity.Position) != 0;
        }

        public int GetInteriorIdAtCords(Vector3 position)
        {
            return API.GetInteriorAtCoords(position.X, position.Y, position.Z);
        }

        public void SetInteriorActive(int interiorId, bool toggle)
        {
            API.SetInteriorActive(interiorId, toggle);
        }

        private static void LoadInterior(int interiorId)
        {
            API.LoadInterior(interiorId);
        }

        public int GetInteriorGroup(int interiorId)
        {
            return API.GetInteriorGroupId(interiorId);
        }

        public void RefreshInterior(int interiorId)
        {
            API.RefreshInterior(interiorId);
        }

        public bool IsInteriorDisabled(int interiorId)
        {
            return API.IsInteriorDisabled(interiorId);
        }

        public void DisableInterior(int interiorId, bool toggle)
        {
            API.DisableInterior(interiorId, toggle);
        }

        public bool IsInteriorReady(int interiorId)
        {
            return API.IsInteriorReady(interiorId);
        }

        public bool IsEntityInInterior(int interiorId, Entity entity)
        {
            return API.GetInteriorFromEntity(entity.Handle) == interiorId;
        }

        private static Vector3 GetInteriorRotation(int interiorId)
        {
            float x = 0;
            float y = 0;
            float z = 0;
            float wd = 0;
            API.GetInteriorRotation(interiorId, ref x, ref y, ref z, ref wd);
            return new Vector3(x, y, z);
        }

        private static Vector3 GetInteriorPosition(int interiorId)
        {
            float x = 0;
            float y = 0;
            float z = 0;
            API.GetInteriorPosition(interiorId, ref x, ref y, ref z);
            return new Vector3(x, y, z);
        }


        public Vector3 ConvertLocalToWorld(Vector3 originPosition, Vector3 originRotation, Vector3 localPosition)
        {
            var radi = Math.PI / 180 * originRotation.Z;
            var rotation = Quaternion.RotationAxis(new Vector3(0, 0, 1), (float)radi);
            var rotatedVector = Vector3.Transform(localPosition, rotation);
            var newVector = Vector3.Add(rotatedVector, originPosition);
            return newVector;
        }

        public Vector3 ConvertWorldToLocal(Vector3 originPosition, Vector3 originRotation, Vector3 worldPosition)
        {
            var radi = -Math.PI / 180 * originRotation.Z;
            var newVector = Vector3.Subtract(worldPosition, originPosition);
            var rotation = Quaternion.RotationAxis(new Vector3(0, 0, 1), (float)radi);
            var rotatedVector = Vector3.Transform(newVector, rotation);
            return rotatedVector;
        }

        #endregion
    }
}