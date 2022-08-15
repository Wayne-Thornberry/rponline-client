using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class MoneyBagController
    {
        private bool _updated;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("MoneyBagController") > 1)
                return;

            while (!token.IsCancellationRequested)
            {
                if (EngineAPI.GetEventExitsts(this, "CEventNetworkPlayerCollectedAmbientPickup"))
                {
                    var test = EngineAPI.GetEventData(this, "CEventNetworkPlayerCollectedAmbientPickup");
                    foreach (var item in test)
                    {
                        EngineAPI.LogDebug(item);
                    }
                    var id = int.Parse(test[3].ToString());
                    var money = int.Parse(test[1].ToString());
                    if (id == API.GetHashKey("xm_prop_x17_bag_01b"))
                    {
                        if (API.GetPedDrawableVariation(Game.PlayerPed.Handle, 5) != 45)
                        {
                            GiveMoneyBag();
                        }
                        EngineAPI.AddValueToWalletBalance(money);
                    }
                }

                if (EngineAPI.GetCharacterWalletBalance() > EngineAPI.GetCharacterMaxWalletBalance())
                {
                    var _value = (int)(EngineAPI.GetCharacterWalletBalance() - EngineAPI.GetCharacterMaxWalletBalance());
                    EngineAPI.SubtractValueFromWalletBalance(_value);
                    var pickup = await World.CreateAmbientPickup(PickupType.MoneyDepBag, Game.PlayerPed.Position + (Game.PlayerPed.ForwardVector * 2), new Model("xm_prop_x17_bag_01b"), _value);
                    pickup.AttachBlip();
                    pickup.IsPersistent = true;
                }

                if (API.GetPedDrawableVariation(Game.PlayerPed.Handle, 5) == 45 && !_updated)
                {
                    EngineAPI.SetCharacterMaxWalletBalance(1000000);
                    _updated = true;
                }
                else if (API.GetPedDrawableVariation(Game.PlayerPed.Handle, 5) != 45 && _updated)
                {
                    EngineAPI.SetCharacterMaxWalletBalance(1000);
                    _updated = false;
                }

                await BaseScript.Delay(0);
            }
        }

        private void GiveMoneyBag()
        {
            API.SetPedComponentVariation(Game.PlayerPed.Handle, 5, 45, 0, 0);
        }
    }
}
