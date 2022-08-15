using Proline.ClassicOnline.CNetConnection.Events;
using System.Threading.Tasks;


namespace Proline.ClassicOnline.CNetConnection.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            PlayerJoinedEvent.SubscribeEvent();
        }
    }
}