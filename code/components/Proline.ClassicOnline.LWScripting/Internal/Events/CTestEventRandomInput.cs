using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Events
{
    internal class CTestEventRandomInput
    {
        public void OnEventInvoked(object[] args)
        {
            Console.WriteLine(this.GetType().Name + " Invoked");

        }
    }
}
