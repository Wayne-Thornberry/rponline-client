
using CShopCatalogue;

namespace RPOnline.Parts
{
    public static partial class EngineAPI
    {

        public static void BuyVehicle(string vehicleName)
        {
            var api = new CShopCatalogueAPI();
            api.BuyVehicle(vehicleName);
            // some metrics and audits
        }
    }
}
