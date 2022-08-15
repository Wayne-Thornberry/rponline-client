using System.Collections.Generic;

namespace Proline.ClassicOnline.CCoreSystem.Internal
{
    internal class ListOfLiveScripts : List<LiveScript>
    {
        private static ListOfLiveScripts _instance;
        internal static ListOfLiveScripts GetInstance()
        {
            if (_instance == null)
                _instance = new ListOfLiveScripts();
            return _instance;
        }

    }
}
