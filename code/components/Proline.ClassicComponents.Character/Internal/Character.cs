using CitizenFX.Core;
using Proline.ClassicOnline.CGameLogic.Data;

namespace Proline.ClassicOnline.CGameLogic.Internal
{
    internal static class Character
    {
        internal static int MaxWalletCapacity { get; set; }
        internal static Entity PersonalVehicle { get; set; }
        internal static PlayerCharacter PlayerCharacter { get; set; }
        internal static long BankBalance { get; set; }
        internal static long WalletBalance { get; set; }
    }
}
