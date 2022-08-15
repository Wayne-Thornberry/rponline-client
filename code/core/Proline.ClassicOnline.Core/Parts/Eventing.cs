using Proline.ClassicOnline.CCoreSystem;

namespace Proline.ClassicOnline.Engine.Parts
{
    public static partial class EngineAPI
    {
        public static void TriggerScriptEvent(string eventName, params object[] args)
        {
            var coreAPI = new CCoreSystemAPI();
            coreAPI.TriggerScriptEvent(eventName, args);
        }

        public static bool GetEventExitsts(object scriptInstance, string eventName)
        {
            var coreAPI = new CCoreSystemAPI();
            return coreAPI.GetEventExitsts(scriptInstance, eventName);
        }

        public static object[] GetEventData(object scriptInstance, string eventName)
        {
            var coreAPI = new CCoreSystemAPI();
            return coreAPI.GetEventData(scriptInstance, eventName);
        }
    }
}
