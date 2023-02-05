using Proline.ClassicOnline.EventQueue;
using Proline.Resource;
using Proline.Resource.Framework;
using System;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace OnlineEngine
{
    public class Program : ResourceScript
    {
        public override async Task OnLoad()
        {
            Console.WriteLine($"Started Engine");
            try
            {
                Component.InitializeComponents();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
