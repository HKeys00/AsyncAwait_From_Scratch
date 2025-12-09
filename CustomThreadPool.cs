using System.Collections.Concurrent;

namespace AsyncAwait_From_Scratch
{

    //MyThreadPool - Look at performance, implement non fixed number of threads, thread management - decreasing and increasing the number of threads
    // - Queue work items
    // - Create and start threads
    // - Implement Execution context with AsyncLocal see if you can't implement your own ExecutionContext instead of using the base class.
    public static class CustomThreadPool
    {
        private static readonly int MaxThreadCount = Environment.ProcessorCount;
        private static readonly BlockingCollection<(Action, string?)> _queue = new BlockingCollection<(Action, string?)>();
        private static readonly ConcurrentBag<CustomThread> _pool = new ConcurrentBag<CustomThread>();

        static CustomThreadPool()
        {
            for (int i = 0; i < MaxThreadCount; i++)
            {
                _pool.Add(new CustomThread(_queue));
            }
        }

        public static void QueueThreadWorkItem(Action action, string contextKey)
        {
            _queue.Add(action);
        }
    }
}
