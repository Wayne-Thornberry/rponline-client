﻿using CitizenFX.Core;
using Proline.ClassicOnline.Engine.Parts;
using System.Threading;
using System.Threading.Tasks;
namespace LevelScripts.UI
{
    public class UIInteractionMenuNew
    {
        public UIInteractionMenuNew()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (EngineAPI.GetInstanceCountOfScript("UIInteractionMenuNew") > 1)
                return;


            while (!token.IsCancellationRequested)
            {

                await EngineAPI.Delay(0);
            }
        }
    }
}
