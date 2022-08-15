using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Internal
{
    internal class ScriptTypeLibrary : Dictionary<string, Type>
    {
        private static ScriptTypeLibrary _instance;

        public static bool HasLoadedScripts { get; internal set; }

        public static ScriptTypeLibrary GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptTypeLibrary();
            return _instance;
        }

        internal Type GetScriptType(string scriptName)
        {
            if (string.IsNullOrEmpty(scriptName)) return null;
            if (ContainsKey(scriptName))
                return this[scriptName];
            return null;
        }
        internal bool DoesScriptTypeExist(string scriptName)
        {
            return ContainsKey(scriptName);
        }

        internal void ProcessAssembly(string assemblyString)
        {
            Console.WriteLine($"Loading assembly {assemblyString.ToString()}");
            var ass = Assembly.Load(assemblyString.ToString());
            Console.WriteLine($"Scanning assembly {assemblyString.ToString()} for scripts");
            var types = ass.GetTypes().Where(e => (object)e.GetMethod("Execute") != null);
            Console.WriteLine($"Found {types.Count()} scripts that have an execute method");
            foreach (var item in types)
            {
                if (!ContainsKey(item.Name))
                    Add(item.Name, item);
                else
                    Console.WriteLine($"{item.Name} DUPLICATE?????");

            }
            Console.WriteLine($"Loading complete");
        }

    }
}
