﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Threading.Tasks;

namespace CScreenRendering.Old
{
    public class PedHeadshot : IDisposable
    {
        public int PedHandle { get; private set; }
        public int Handle { get; private set; }
        public string HeadShotTXT => API.GetPedheadshotTxdString(Handle);
        public bool HasLoaded => API.IsPedheadshotReady(Handle);
        public bool IsValid => API.IsPedheadshotValid(Handle);

        public PedHeadshot(int pedHandle)
        {
            PedHandle = pedHandle;
        }

        public void Dispose()
        {
            if (IsValid && HasLoaded)
                API.UnregisterPedheadshot(Handle);
        }

        public async Task LoadHeadShot()
        {
            API.RegisterPedheadshot(Game.PlayerPed.Handle);
            while (!HasLoaded)
            {
                await BaseScript.Delay(0);
            }
        }

    }
}
