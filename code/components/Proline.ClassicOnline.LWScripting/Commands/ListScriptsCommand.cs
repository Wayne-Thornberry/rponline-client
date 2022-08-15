using Proline.ClassicOnline.CCoreSystem.Internal;
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
            var sm = ScriptTypeLibrary.GetInstance();
            foreach (var item in sm.Keys)
            {
                Console.WriteLine(item);
            }
        }
    }
}
