using Proline.ClassicOnline.CNativeEvents.Events;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CNativeEvents.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            GameEventTriggered.SubscribeEvent();
        }
    }
}