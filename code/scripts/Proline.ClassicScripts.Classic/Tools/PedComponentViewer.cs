using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic.Tools
{
    public class PedComponentViewer
    {
        private int _componentId;
        private int _drawableId;
        private int _textureId;
        private int _palletId;

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("PedComponentViewer") > 1)
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
                    var max = API.GetNumberOfPedTextureVariations(Game.PlayerPed.Handle, _componentId, _drawableId);
                    _textureId++;
                    if (_textureId > max)
                    {
                        _textureId = max;
                    }
                    justPressed = true;
                }
                if (justPressed)
                {
                    API.SetPedDefaultComponentVariation(Game.PlayerPed.Handle);
                    API.SetPedComponentVariation(Game.PlayerPed.Handle, _componentId, _drawableId, _textureId, _palletId);
                    Screen.ShowSubtitle($"Component {_componentId} Drawable {_drawableId} Texture: {_textureId} Pallet: {_palletId} Name: {API.GetHashNameForProp(Game.PlayerPed.Handle, _componentId, _drawableId, _textureId).ToString()}");
                    justPressed = false;
                }

                await BaseScript.Delay(0);
            }
        }
    }
}
