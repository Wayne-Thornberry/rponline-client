using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.CCoreSystem.Internal
{
    internal class LiveScript
    {
        public Type InstanceType => Instance.GetType();
        public string Name => InstanceType.Name;
        public CancellationTokenSource CancelToken => _tokenSource;
        public bool IsCompleted => ExecutionTask != null ? ExecutionTask.IsCompleted : false;
        public Task ExecutionTask => _executionTask;
        public int Id => ExecutionTask == null ? -1 : ExecutionTask.Id;
        public bool IsMarkedForNolongerNeeded { get; internal set; }
        public object Instance => _instance;
        public string InstanceId => _instanceId;
        private Dictionary<string, Queue<InvokedEvent>> _eventQueue;
        private Task _executionTask;



        private CancellationTokenSource _tokenSource;
        private object _instance;
        private string _instanceId;

        public LiveScript(object instance)
        {
            _instance = instance;
            _instanceId = Guid.NewGuid().ToString();
            _eventQueue = new Dictionary<string, Queue<InvokedEvent>>();
        }

        internal void Terminate()
        {
            if (!IsCompleted)
            {
                CancelToken.Cancel();
            }
        }

        internal void Execute(params object[] args)
        {
            _tokenSource = new CancellationTokenSource();
            var method = InstanceType.GetMethod("Execute");
            Console.WriteLine(string.Format("{0} Script Started", Name));
            _executionTask = (Task)method.Invoke(_instance, new object[] { args, _tokenSource.Token });
            Console.WriteLine(string.Format("{0} Executed Succesfully, Running", Name));
        }

        internal bool HasEvent(string eventName)
        {
            return _eventQueue.ContainsKey(eventName);
        }

        internal object[] DequeueEvent(string eventName)
        {
            if (_eventQueue.ContainsKey(eventName))
            {
                var args = _eventQueue[eventName].Dequeue().Args;
                if (_eventQueue[eventName].Count == 0)
                {
                    _eventQueue.Remove(eventName);
                }
                return args;
            }
            return null;
        }

        internal void EnqueueEvent(string eventName, params object[] args)
        {
            if (!_eventQueue.ContainsKey(eventName))
                _eventQueue.Add(eventName, new Queue<InvokedEvent>());
            _eventQueue[eventName].Enqueue(new InvokedEvent(eventName, args));
        }
    }
}
