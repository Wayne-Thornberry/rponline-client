namespace Proline.ClassicOnline.CCoreSystem.Internal
{
    internal class ScriptTaskTokenManager
    {
        private static ScriptTaskTokenManager _instance;

        public static ScriptTaskTokenManager GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptTaskTokenManager();
            return _instance;
        }
    }
}
