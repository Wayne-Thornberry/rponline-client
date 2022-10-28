﻿using Proline.ClassicOnline.CCoreSystem.Internal;
using Proline.Resource.Framework;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Commands
{
    public class ListScriptsCommand : ResourceCommand
    {
        public ListScriptsCommand() : base("ListScripts")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        { 
            foreach (var item in ScriptManager.GetScriptNames())
            {
                Console.WriteLine(item);
            }
        }
    }
}
