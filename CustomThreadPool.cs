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
        private static readonly int MaxThreadCount = Environment.ProcessorCount;

        private static readonly Queue<Action> _queue = new Queue<Action>();
        private static readonly ConcurrentBag<CustomThread> _pool = new ConcurrentBag<CustomThread>();

        static CustomThreadPool()
        {
            //TODO: Create and start threads here
        }

        public static void QueueThreadWorkItem(Action action)
        {
            if (_queue.Count > _pool.Count)
            {
                if (_pool.Count <= MaxThreadCount)
                {
                    var thread = new CustomThread(ThreadClearOfWork);
                    thread.SetTask(action);
                }


            if (_queue.Count > _pool.Count)
            {
                _queue.Enqueue(action);
            }
            
            
        }

        private static void ThreadClearOfWork()
        {

        }
    }
}
