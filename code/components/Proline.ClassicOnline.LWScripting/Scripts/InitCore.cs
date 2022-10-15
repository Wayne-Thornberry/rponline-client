using CitizenFX.Core;
using Proline.ClassicOnline.CCoreSystem.Events;
using Proline.ClassicOnline.CCoreSystem.Internal;
using Proline.ClassicOnline.CCoreSystem.Tasks;
using Proline.ClassicOnline.EventQueue;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LevelScripts
{
    public class InitCore
    {  
        public async Task Execute()
        {
            //PlayerJoinedEvent.SubscribeEvent();
            PlayerReadyEvent.SubscribeEvent();
             
            ComponentEvent.RegisterEvent(typeof(CTestEventRandomInput));

            TaskManager.Enable(); 

        }
    }
}
