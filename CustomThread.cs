using System.Collections.Concurrent;

namespace AsyncAwait_From_Scratch
{
    public class CustomThread
    {
        private Thread _thread;

        private BlockingCollection<(Action, ExecutionContext?)> _collection;

        public CustomThread(BlockingCollection<(Action, ExecutionContext?)> collection)
        {
            _collection = collection;
            _thread = new Thread(() =>
            {
                while (true)
                {
                    (var action, var context) = _collection.Take();
                    if (action != null)
                    {
                        if (context == null)
                        {
                            action();
                        } else
                        {
                            ExecutionContext.Run(context, state => ((Action)state!).Invoke(), action);
                        }
                        
                    }

                    action = null;
                }
            });
            _thread.IsBackground = true;
            _thread.Start();
        }
    }
}
