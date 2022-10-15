using Proline.ClassicOnline.CCoreSystem.Internal;
using Proline.Resource.Framework;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Commands
{
    public class ListAllScriptTasksCommand : ResourceCommand
    {
        public ListAllScriptTasksCommand() : base("ListAllScriptTasks")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var sm = TaskManager.GetInstance();
            foreach (var scriptTask in sm.GetAllTasks())
            {
                Console.WriteLine(string.Format("Task Id {0}, Is Complete {1}, Status {2} ", scriptTask.Id, scriptTask.IsCompleted, scriptTask.Status));
            }
        }
    }
}
