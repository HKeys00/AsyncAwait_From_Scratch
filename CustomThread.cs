using System.Collections.Concurrent;

namespace AsyncAwait_From_Scratch
{
    public class CustomThread
    {
        private Thread _thread;
        private Action? _action;
        private string? _contextKey;

        private BlockingCollection<(Action, string?)> _collection;

        public CustomThread(BlockingCollection<(Action, string?)> collection)
        {
            _collection = collection;
            _thread = new Thread(() =>
            {
                while (true)
                {
                    (_action, _contextKey) = _collection.Take();
                    if (_action != null)
                    {
                        _action();
                    }

                    _action = null;
                }
            });
            _thread.IsBackground = true;
            _thread.Start();
        }
    }
}
