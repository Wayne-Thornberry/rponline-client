﻿using CitizenFX.Core;
using System.Threading;
using System.Threading.Tasks;

namespace LevelScripts
{
    public class BlowUp
    {
        public BlowUp()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            if (args.Length > 0)
            {
                var handle = (int)args[0];
                var entity = Entity.FromHandle(handle);
                World.AddExplosion(entity.Position, ExplosionType.Car, 2f, 0f);
            }
        }
    }
}
