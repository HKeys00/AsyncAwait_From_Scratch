using System.Collections.Concurrent;

namespace AsyncAwait_From_Scratch
{
    public class CustomThread
    {
        private Thread _thread;
        private Action? _action;
        private ExecutionContext? _context;

        private BlockingCollection<(Action, ExecutionContext?)> _collection;

        public CustomThread(BlockingCollection<(Action, ExecutionContext?)> collection)
        {
            _collection = collection;
            _thread = new Thread(() =>
            {
                while (true)
                {
                    (_action, _context) = _collection.Take();
                    if (_action != null)
                    {
                        if (_context == null)
                        {
                            _action();
                        } else
                        {
                            ExecutionContext.Run(_context, state => ((Action)state!).Invoke(), _action);
                        }
                        
                    }

                    _action = null;
                }
            });
            _thread.IsBackground = true;
            _thread.Start();
        }
    }
}
