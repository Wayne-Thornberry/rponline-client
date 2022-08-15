using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Tools
{
    public class PedPropViewer
    {
        private int _componentId;
        private int _drawableId;
        private int _textureId;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("PedPropViewer") > 1)
                return;

            bool justPressed = false;
            while (!token.IsCancellationRequested)
            {
                if (Game.IsControlJustPressed(0, Control.FrontendUp))
                {

                    _componentId++;
                    _drawableId = 0;
                    _textureId = 0;
                    if (_drawableId > 15)
                    {
                        _drawableId = 15;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendDown))
                {
                    _componentId--;
                    _drawableId = 0;
                    _textureId = 0;
                    if (_componentId < 0)
                    {
                        _componentId = 0;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendLeft))
                {
                    _drawableId--;
                    _textureId = 0;
                    if (_drawableId < 0)
                    {
                        _drawableId = 0;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendRight))
                {
                    var max = API.GetNumberOfPedDrawableVariations(Game.PlayerPed.Handle, _componentId);
                    _drawableId++;
                    _textureId = 0;
                    if (_drawableId > max)
                    {
                        _drawableId = max;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendAccept))
                {
                    _textureId--;
                    if (_textureId < 0)
                    {
                        _textureId = 0;
                    }
                    justPressed = true;
                }
                else if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                {
                    var max = API.GetNumberOfPedPropTextureVariations(Game.PlayerPed.Handle, _componentId, _drawableId);
                    _textureId++;
                    if (_textureId > max)
                    {
                        _textureId = max;
                    }
                    justPressed = true;
                }
                if (justPressed)
                {
                    API.ClearAllPedProps(Game.PlayerPed.Handle);
                    API.SetPedPropIndex(Game.PlayerPed.Handle, _componentId, _drawableId, _textureId, true);
                    Screen.ShowSubtitle($"Component {_componentId} Drawable {_drawableId} Texture: {_textureId} Name: {API.GetHashNameForProp(Game.PlayerPed.Handle, _componentId, _drawableId, _textureId).ToString()}");
                    justPressed = false;
                }

                await BaseScript.Delay(0);
            }
        }
    }
}
