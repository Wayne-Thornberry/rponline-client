using CitizenFX.Core;
using Proline.ClassicOnline.CCoreSystem.Tasks;
using Proline.ClassicOnline.EventQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.CCoreSystem.Internal
{

    internal class TaskManager
    {
        private static TaskManager _instance;
        private static List<Task> _runningTasks; 
        private static bool _enableProcessing;

        public static TaskManager GetInstance()
        {
            if (_instance == null)
                _instance = new TaskManager();
            return _instance;
        }

        internal IEnumerable<Task> GetAllTasks()
        {
            var sm = ListOfLiveScripts.GetInstance();
            return sm.Select(e => e.ExecutionTask);
        }

        internal static Task StartNew(Func<Task> taskFunc)
        {
            UpdateTaskList();
            var task = Task.Factory.StartNew(taskFunc);
            _runningTasks.Add(task);
            return task;
        }

        internal static Task StartNew(Action taskFunc)
        {
            UpdateTaskList();
            var task = Task.Factory.StartNew(taskFunc);
            _runningTasks.Add(task);
            return task;
        }

        private static void UpdateTaskList()
        {
            var tasks = _runningTasks.ToArray();
            var count = 0;
            foreach (var task in tasks)
            {
                if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.WaitingForActivation)
                {
                    _runningTasks.Remove(task);
                    count++;
                }
            }
        }

        internal static void Enable()
        {
            if (!_enableProcessing)
            { 
                _enableProcessing = true;
                StartNew(GetInstance().TaskProcesser);
            }
        }

        internal static void Disable()
        {
            if (_enableProcessing)
            { 
                _enableProcessing = false; 
            }
        }

        private async Task TaskProcesser()
        {
            var gc = new GarbageCleaner();
            var _eventProcessor = new ComponentEventProcessor();
            while (_enableProcessing)
            {
                await _eventProcessor.ProcessQueue();
                await gc.Execute();
                await BaseScript.Delay(1000);
            }
        }
    }
}
