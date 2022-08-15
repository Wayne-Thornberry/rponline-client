using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Events
{
    public class CEventNetworkVehicleDestroyed
    {
        public void OnEventInvoked(object[] args)
        {
            Console.WriteLine(this.GetType().Name + " Invoked");
        }
    }
}
