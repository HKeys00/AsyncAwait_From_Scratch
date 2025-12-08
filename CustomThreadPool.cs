using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait_From_Scratch
{

    //MyThreadPool - Look at performance, implement non fixed number of threads, thread management - decreasing and increasing the number of threads
    // - Queue work items
    // - Create and start threads
    // - Implement Execution context with AsyncLocal see if you can't implement your own ExecutionContext instead of using the base class.
    public static class CustomThreadPool
    {
        private static readonly object _lock = new();
        private static readonly int MaxThreadCount = Environment.ProcessorCount - 1;
        private static readonly Queue<Action> _queue = new Queue<Action>();
        private static readonly ConcurrentBag<CustomThread> _pool = new ConcurrentBag<CustomThread>();
        private static readonly Thread _mainThread = new Thread(() => {
            while (true)
            {
                ThreadClearOfWork();
            }
        });


        static CustomThreadPool()
        {
            _mainThread.Start();
            //TODO: Create and start threads here
        }

        public static void QueueThreadWorkItem(Action action)
        {
            _queue.Enqueue(action);
        }

        private static void ThreadClearOfWork()
        {
            if (_queue.Count > 0)
            {
                RunWorkItemOnThread();
            }
        }

        private static void RunWorkItemOnThread() {
            lock (_lock)
            {
                var thread = _pool.FirstOrDefault(t => !t.IsRunning);
                if (thread == null)
                {
                    if (_pool.Count < MaxThreadCount)
                    {
                        thread = new CustomThread();
                        thread.SetTask(_queue.Dequeue());
                        _pool.Add(thread);
                    }

                    return;
                }

                thread.SetTask(_queue.Dequeue());
            }

        }
    }
}
