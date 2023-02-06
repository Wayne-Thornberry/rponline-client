using CitizenFX.Core;
using CitizenFX.Core.UI;
using RPOnline.Parts;
using System.Threading;
using System.Threading.Tasks;


namespace LevelScripts
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
            await EngineAPI.Delay(1000);
            Screen.LoadingPrompt.Hide();
        }
    }
}
