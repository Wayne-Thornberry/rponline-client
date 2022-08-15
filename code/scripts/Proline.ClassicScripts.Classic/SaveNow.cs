using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;


namespace Proline.ClassicOnline.SClassic
{
    public class SaveNow
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("SaveNow") > 1)
                return;
            Screen.LoadingPrompt.Show("Saving...", LoadingSpinnerType.SocialClubSaving);
            await EngineAPI.SendSaveToCloud();
            await BaseScript.Delay(1000);
            Screen.LoadingPrompt.Hide();
        }
    }
}
