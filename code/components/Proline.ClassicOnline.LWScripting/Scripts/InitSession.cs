using Proline.ClassicOnline.EventQueue;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CCoreSystem.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            ComponentEvent.InvokeEvent("CTestEventRandomInput");
            ComponentEvent.InvokeEvent("CPublicTestEventRandomInput");
        }
    }
}
