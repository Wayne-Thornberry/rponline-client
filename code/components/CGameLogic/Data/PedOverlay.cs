﻿namespace CGameLogic.Data
{
    public struct PedOverlay
    {
        public int Index { get; set; }
        public float Opacity { get; set; }
        public int ColorType { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
    }


    //public class Character
    //{
    //    public int Id { get; set; }

    //    public CharacterBrief Brief { get; set; }
    //    public CharacterStats Stats { get; set; }
    //    public CharacterPed Ped { get; set; } = new CharacterPed();
    //}

    //public class CharacterPed
    //{
    //    public string Name { get; set; } = "NEW CHARACTER";
    //    public char Gender { get; set; } = 'm';
    //    public string SpawnLocation { get; set; } = "LAST_LOCATION";
    //    public float[] LastPosition { get; set; } = { 0f, 0f, 70f };
    //    public bool IsDead { get; set; }
    //    public bool IsArrested { get; set; }
    //    public PedLooks Looks { get; set; } = new PedLooks();
    //    public PedOutfit Outfit { get; set; } = new PedOutfit();
    //    public PedLoadout Loadout { get; set; } = new PedLoadout();
    //}
}
