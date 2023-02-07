using CDataStream;
using CGameLogic;
using CitizenFX.Core;
using Proline.Resource.Framework;
using System;

namespace RPOnlineGame.Commands
{
    public class BuyRandomWeaponCommand : ResourceCommand
    {
        public BuyRandomWeaponCommand() : base("BuyRandomWeapon")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var EngineAPI = new CGameLogicAPI();
            if (EngineAPI.GetCharacterBankBalance() > 250)
            {

                Array values = Enum.GetValues(typeof(WeaponHash));
                Random random = new Random();
                WeaponHash randomBar = (WeaponHash)values.GetValue(random.Next(values.Length));
                var ammo = random.Next(1, 250);
                Game.PlayerPed.Weapons.Give(randomBar, ammo, true, true);

                var id = "PlayerWeapon";
                var dataAPI = new CDataStreamAPI();
                dataAPI.CreateDataFile();
                dataAPI.AddDataFileValue("WeaponHash", randomBar);
                dataAPI.AddDataFileValue("WeaponAmmo", ammo);
                dataAPI.SaveDataFile(id);

                EngineAPI.SubtractValueFromBankBalance(250);
            }
        }
    }
}
