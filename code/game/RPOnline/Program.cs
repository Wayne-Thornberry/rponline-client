using Proline.ClassicOnline.Resource;
using RPOnlineTests.Tests;
using System.Threading.Tasks;

namespace RPOnlineGame
{
    public class Program : ResourceMainScript
    {
        //// 
        /// Expose your resource here
        /// Utilize exports
        /// Resource handlers
        /// anything on a resource level
        public override async Task OnStart()
        {
            base.OnStart();
            var test = new BasicTests();
            test.BasicSuccessTest();
            test.BasicFailureTest();
        }
    }
}
