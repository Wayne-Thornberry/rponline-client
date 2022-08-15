using CitizenFX.Core;
using Proline.ClassicOnline.CCoreSystem.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Tasks
{
    public class GarbageCleaner
    {
        public GarbageCleaner()
        {

        }

        public async Task Execute()
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();

                var instances = sm.Where(e => e.IsMarkedForNolongerNeeded).ToArray();
                if (instances.Count() > 0)
                    Console.WriteLine(string.Format("Several scripts have been marked as no longer needed {0}, terminating them", instances.Count()));
                foreach (var instance in instances)
                {
                    instance.Terminate();
                    Console.WriteLine(string.Format("Attempting to stop script {0} because it was marked as no longer needed", instance.Name));
                    while (!instance.IsCompleted)
                        await BaseScript.Delay(0);
                    // Cleanup the stuff that the script took ownership off
                    Console.WriteLine(string.Format("all script tasks {0} have been terminated", instance.Name));
                }

                instances = sm.Where(e => e.IsCompleted).ToArray();
                if (instances.Count() > 0)
                    Console.WriteLine(string.Format("Removing {0} script instances that have been completed", instances.Count()));
                int count = 0;
                foreach (var instance in instances)
                {
                    var instanceCount = sm.Remove(instance);
                    count++;
                    Console.WriteLine(string.Format("{0} Removed {1} instances", instance.Name, 1));
                }
                if (instances.Count() > 0)
                    Console.WriteLine(string.Format("Removed {0} script instances that have been completed", count));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
