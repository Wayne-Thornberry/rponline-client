using Proline.ClassicOnline.CCoreSystem.Internal;
using Proline.ClassicOnline.CDebugActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
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

        public bool GetEventExists(object scriptInstance, string eventName)
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


                // Get scripting config
                var scriptingConfig = ScriptingConfigSection.ModuleConfig;

                // If we have a config, then we can load the levelscripts
                var scriptTypes = new Dictionary<string, Type>();
                if (scriptingConfig != null)
                {
                    // Load Assemblies 
                    Console.WriteLine("Retrived config section");
                    Console.WriteLine($"Loading level scripts. from {scriptingConfig.LevelScriptAssemblies.Count()} assemblies");
                    foreach (var assemblyStrings in scriptingConfig.LevelScriptAssemblies)
                    {
                        Console.WriteLine($"Loading assembly {assemblyStrings}");
                        var assembly = Assembly.Load(assemblyStrings.ToString());
                        Console.WriteLine($"Scanning assembly {assemblyStrings} for scripts");
                        var types = assembly.GetTypes().Where(e => (object)e.GetMethod("Execute") != null);
                        Console.WriteLine($"Found {types.Count()} scripts that have an execute method");
                        foreach (var item in types)
                        {
                            if (!scriptTypes.ContainsKey(item.Name))
                                scriptTypes.Add(item.Name, item);
                            else
                                Console.WriteLine($"{item.Name} DUPLICATE?????");

                        }
                        Console.WriteLine($"Loading complete");
                    }
                }
                else
                {
                    Console.WriteLine("Cannot start script {0} because the config failed to load or is not set");
                    return -1;
                }

                // Get type matching script name
                if (!scriptTypes.ContainsKey(scriptName))
                {
                    Console.WriteLine("Script {0} cannot be found");
                    return -1;
                }

                // Get the script type from the dictionary that was created
                var type = scriptTypes[scriptName];

                Console.WriteLine(String.Format("Creating script instance of {0}", scriptName));

                // Create an instance of that type found. There should be no constructor on these scripts
                var instance = Activator.CreateInstance(type);

                if (instance == null)
                {
                    Console.WriteLine(String.Format("Unable to create script instance of {0}, instance came back null", scriptName));
                    return -1;
                }

                // Create a shell for the script
                var script = new LiveScript(instance);
                sm.Add(script);

                // We need to create a cacellation token that can stop the base while loop in the scripts 
                Console.WriteLine(string.Format("{0} Script Started", scriptName));
                script.Start();
                Console.WriteLine(string.Format("{0} Executed Succesfully, Running", scriptName));  
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
