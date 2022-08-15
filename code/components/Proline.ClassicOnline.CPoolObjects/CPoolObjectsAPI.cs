using Proline.ClassicOnline.CPoolObjects.Internal;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CPoolObjects
{
    public class CPoolObjectsAPI : ICPoolObjectsAPI
    {
        public int[] GetAllExistingPoolObjects()
        {
            return PoolObjectManager.TrackedHandles;
        }
    }
}
