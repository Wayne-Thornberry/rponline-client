using CitizenFX.Core;
using Proline.ClassicOnline.CCoreSystem.Events;
using Proline.ClassicOnline.CCoreSystem.Internal;
using Proline.ClassicOnline.CCoreSystem.Tasks;
using Proline.ClassicOnline.EventQueue;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CCoreSystem.Scripts
{
    public class InitCore
    {
        private ComponentEventProcessor _eventProcessor;

        public async Task Execute()
        {
            //PlayerJoinedEvent.SubscribeEvent();
            PlayerReadyEvent.SubscribeEvent();

            _eventProcessor = new ComponentEventProcessor(); 
            ComponentEvent.RegisterEvent(typeof(CTestEventRandomInput));

            var lsAssembly = ScriptingConfigSection.ModuleConfig;
            Console.WriteLine("Retrived config section");
            var _lwScriptManager = ScriptTypeLibrary.GetInstance();

            if (lsAssembly != null)
            {
                Console.WriteLine($"Loading level scripts. from {lsAssembly.LevelScriptAssemblies.Count()} assemblies");
                foreach (var item in lsAssembly.LevelScriptAssemblies)
                {
                    _lwScriptManager.ProcessAssembly(item);
                }
                ScriptTypeLibrary.HasLoadedScripts = true;
            }

            var gc = new GarbageCleaner();
            while (true)
            {
                var task1 = Task.Factory.StartNew(_eventProcessor.ProcessQueue);
                var task2 = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(1000);
            }


        }
    }
}
