using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Tools
{
    public class FrontEndPlayer
    {
        private int _index1;
        private int _index2;
        private int _index3;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("FrontEndPlayer") > 1)
                return;

            _index2 = -1;
            bool justPressed = false;
            while (!token.IsCancellationRequested)
            {
                if (Game.IsControlJustPressed(0, Control.FrontendUp))
                {

                    _index1++;
                    _index2 = 0;
                    var max =
                    EngineAPI.GetNumOfAudioSamples();
                    if (_index1 > max)
                    {
                        _index1 = 0;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendDown))
                {
                    _index1--;
                    _index2 = 0;
                    if (_index1 < 0)
                    {
                        _index1 = 0;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendLeft))
                {
                    _index2--;
                    if (_index2 < -1)
                    {
                        _index2 = -1;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendRight))
                {
                    var max = 2000;
                    _index2++;
                    if (_index2 > max)
                    {
                        _index2 = max;
                    }
                    justPressed = true;
                }

                else if (Game.IsControlJustPressed(0, Control.FrontendPause))
                {
                    break;
                }
                if (justPressed)
                {
                    EngineAPI.GetAudioSamplesAtIndex(_index1, out string audioId, out string audioRef, out bool enabled);
                    API.PlaySoundFrontend(_index2, audioId, audioRef, enabled);
                    Screen.ShowSubtitle($"Id: {_index1} AudioName {audioId} AudioRef {audioRef} SoundId: {_index2} ");
                    justPressed = false;
                }

                await BaseScript.Delay(0);
            }
        }
    }
}
