using System.Timers;

namespace AsyncAwait_From_Scratch
{

    // - Implement class MyTask instead of using Task<T>
    // - MyTask needs to know if it's completed and set that it's completed
    // - It needs to be able to set any exceptions that were thrown
    // - It needs to synchronously block and wait for a task to complete
    // - It needs to schedule a continuation to run after the task is complete (bonus if you can chain tasks together)
    // - Needs to implement Task.Run() and Task.WhenAll
    // - Bonus make it generic
    // - Task needs to be thread safe (why is lock(this) bad?)
    // - ManualResetEventSlim
    // - Can you await custom MyTask class.


    public class CustomTask
    {
        private object _lock = new();
        private bool _completed;
        private Exception? _exception;
        private Action? _continuation;
        private ExecutionContext? _context;
        
        public bool Completed { 
            get 
            {
                lock (_lock)
                {
                    return _completed;
                }
            } 
        }

        public void SetResult() => Complete(null);

        public void SetException(Exception exception) => Complete(exception);
        
        private void Complete(Exception? exception)
        {
            lock (_lock)
            {
                if (_completed) throw new Exception("Tried to complete an already completed task");

                _completed = true;
                _exception = exception;

                if (_continuation != null)
                {
                    CustomThreadPool.QueueThreadWorkItem(() =>
                    {
                        if (_context == null)
                        {
                            _continuation();
                        }
                        else
                        {
                            ExecutionContext.Run(_context, state => ((Action)state!).Invoke(), _continuation);
                        }
                    });
                }
            }
        }

        public void Wait()
        {
            ManualResetEventSlim? mres = null;
            lock (_lock)
            {
                if (!_completed)
                {
                    mres = new ManualResetEventSlim();
                    ContinueWith(mres.Set);
                }
            }

            mres?.Wait();

            if (_exception != null)
            {
                throw new AggregateException(_exception);
            }
        }

        public CustomTask ContinueWith(Action action)
        {
            var task = new CustomTask();
            _context = ExecutionContext.Capture();

            Action callback = () =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    task.SetException(e);
                    return;
                }

                task.SetResult();
            };


            lock (_lock)
            {
                if (_completed)
                {
                    CustomThreadPool.QueueThreadWorkItem(callback);
                } else
                {

                    _continuation = callback;
                }

            }

            return task;
        }

        public static CustomTask WhenAll(List<CustomTask> tasks)
        {
            var task = new CustomTask();

            if (tasks.Count == 0)
            {
                task.SetResult();
            } 
            else 
            {
                int count = tasks.Count;
                foreach(var t in tasks)
                {
                    t.ContinueWith(() =>
                    {
                        Interlocked.Decrement(ref count);
                        if (count == 0)
                        {
                            task.SetResult();
                        }
                    });
                }
            }

            return task;
        }

        public static CustomTask Run(Action action)
        {
            var task = new CustomTask();
            CustomThreadPool.QueueThreadWorkItem(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    task.SetException(ex);
                    return;
                }
                     
                task.SetResult();
            });


            return task;
        }

        public static CustomTask Delay(int timeout)
        {
            var task = new CustomTask();

            var timer = new System.Timers.Timer(timeout);
            timer.Elapsed += (s, e) => task.SetResult();
            timer.AutoReset = false;
            timer.Start();

            return task;
        }

        public static CustomTask Iterate(IEnumerable<CustomTask> tasks)
        {
            var task = new CustomTask();

            IEnumerator<CustomTask> iter = tasks.GetEnumerator();
            
            void MoveNext()
            {
                
                if (iter.MoveNext())
                {
                    CustomTask next = iter.Current;
                    next.ContinueWith(MoveNext);
                    return;
                }

                task.SetResult();
            }

            MoveNext();

            return task;
        }
    }
}
