﻿using CitizenFX.Core;

namespace CGameLogic.Data
{
    public class PlayerCharacter : Entity
    {
        public char Gender { get; set; } = 'm';
        public string SpawnLocation { get; set; } = "LAST_LOCATION";
        public float[] LastPosition { get; set; } = { 0f, 0f, 70f };
        public bool IsArrested { get; set; }
        public CharacterLooks Looks { get; set; }
        public CharacterOutfit Outfit { get; set; }
        public CharacterLoadout Loadout { get; set; }
        public CharacterPersonalVehicle PersonalVehicle { get; set; }
        public CharacterStats Stats { get; set; }

        public PlayerCharacter(int handle) : base(handle)
        {
        }
    }
}