﻿using Proline.Resource.Framework;
using RPOnline.Parts;
using System.Linq;

namespace RPOnlineGame.Commands
{
    public class StartNewScriptCommand : ResourceCommand
    {
        public StartNewScriptCommand() : base("StartNewScript")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Count() == 0)
            {
                return;
            }
            var scriptName = args[0].ToString(); 
            EngineAPI.StartNewScript(scriptName);
        }
    }
}
