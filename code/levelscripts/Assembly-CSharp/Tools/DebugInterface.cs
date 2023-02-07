﻿using CitizenFX.Core;
using RPOnline.Parts;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts.Tools
{
    public class DebugInterface
    {
        private List<int> _handles;

        public DebugInterface()
        {
            _handles = new List<int>();
        }


        public async Task Execute(object[] args, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var t = Game.PlayerPed.Position.ToString() + "H:" + Game.PlayerPed.Heading + "\n"
                   + Game.PlayerPed.Model.Hash + "\n"
                   + Game.PlayerPed.Health + "\n"
                   + Game.PlayerPed.Handle + "\n" +
                   _handles.Count + " Entities in the world ";
                EngineAPI.DrawDebugText2D(t, new PointF(0.01f, 0.05f), 0.3f, 0);
                await EngineAPI.Delay(0);
            }
        }
    }
}
