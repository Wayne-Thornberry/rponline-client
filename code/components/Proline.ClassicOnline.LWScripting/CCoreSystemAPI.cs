using Proline.ClassicOnline.CCoreSystem.Internal;
using Proline.ClassicOnline.CDebugActions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem
{
    public class CCoreSystemAPI : ICCoreSystemAPI
    {
        public void TriggerScriptEvent(string eventName, params object[] args)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                foreach (var item in sm)
                {
                    item.EnqueueEvent(eventName, args);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool GetEventExitsts(object scriptInstance, string eventName)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var script = sm.FirstOrDefault(e => e.Instance == scriptInstance);
                return script.HasEvent(eventName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        public object[] GetEventData(object scriptInstance, string eventName)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var script = sm.FirstOrDefault(e => e.Instance == scriptInstance);
                return script.DequeueEvent(eventName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return new object[0];
        }

        public int StartNewScript(string scriptName, params object[] args)
        {

            try
            {
                var api = new CDebugActionsAPI();
                var sm = ListOfLiveScripts.GetInstance();
                var stl = ScriptTypeLibrary.GetInstance();

                if (!stl.DoesScriptTypeExist(scriptName))
                    return -1;

                var type = stl.GetScriptType(scriptName);
                if (type == null)
                {
                    Console.WriteLine(String.Format("Unable to create script instance of {0}, instance type does not exist", scriptName));
                    return -1;
                }
                Console.WriteLine(String.Format("Creating script instance of {0}", scriptName));
                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    Console.WriteLine(String.Format("Unable to create script instance of {0}, instance came back null", scriptName));
                    return -1;
                }

                var script = new LiveScript(instance);
                sm.Add(script);
                script.Execute(args);
                var scriptTask = script.ExecutionTask;
                Console.WriteLine(String.Format("Task Id {0}, Is Complete {1}, Status {2} ", scriptTask.Id, scriptTask.IsCompleted, scriptTask.Status));
                api.LogDebug($"Calling Task ID for API {Task.CurrentId}");
                return script.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return -1;
        }


        public int GetInstanceCountOfScript(string scriptName)


        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var count = sm.Where(e => e.Name.Equals(scriptName)).Count();
                //  Console.WriteLine(String.Format("Getting the instance count of script {0} count: {1}", scriptName, count));
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }

        public void MarkScriptAsNoLongerNeeded(object callingClass)
        {
            try
            {

                var sm = ListOfLiveScripts.GetInstance();
                var script = sm.FirstOrDefault(e => e.Instance == callingClass);
                Console.WriteLine(String.Format("Requesting that script instances by the name of {0} be marked as no longer needed", script.Name));
                script.IsMarkedForNolongerNeeded = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void MarkScriptAsNoLongerNeeded(string scriptName)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var cls = scriptName;
                Console.WriteLine(String.Format("Requesting that all script instances by the name of {0} be marked as no longer needed", scriptName));
                var scripts = sm.Where(e => e.Name.Equals(scriptName));
                foreach (var item in scripts)
                {
                    item.IsMarkedForNolongerNeeded = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Terminates all script instances with the passed scriptName
        /// </summary>
        /// <param name="scriptName"></param>
        public void TerminateScript(string scriptName)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var scripts = sm.Where(e => e.Name.Equals(scriptName));
                Console.WriteLine(String.Format("Requesting that all script instances by the name of {0} be terminated", scriptName));
                foreach (var script in scripts)
                {
                    script.Terminate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Terminates the passed script object task instance
        /// </summary>
        /// <param name="scriptInstance"></param>
        public void TerminateScriptInstance(object scriptInstance)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var script = sm.FirstOrDefault(e => e.Instance == scriptInstance);
                if (script == null)
                    return;
                Console.WriteLine(String.Format("Requesting that a specific script instances by the name of {0} be terminated", script.Name));
                script.Terminate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        public void TerminateScriptTask(int taskId)
        {
            try
            {
                var sm = ListOfLiveScripts.GetInstance();
                var script = sm.FirstOrDefault(e => e.ExecutionTask.Id == taskId);
                if (script == null)
                    return;
                Console.WriteLine(String.Format("Requesting that a specific script instances by the name of {0} be terminated", script.Name));
                script.Terminate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
