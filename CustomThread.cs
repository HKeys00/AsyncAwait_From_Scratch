namespace AsyncAwait_From_Scratch
{
    public class CustomThread
    {
        private AutoResetEvent _signal;

        private Thread _thread;
        private Action? _action;
        private Action _completeCallback;

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public CustomThread(Action completeCallback)
        {
            _isRunning = false;
            _completeCallback = completeCallback;

            _signal = new AutoResetEvent(false);
            _thread = new Thread(() =>
            {
                while (true)
                {
                    _signal.WaitOne();
                    if (_action != null)
                    {
                        _isRunning = true;
                        _action();
                    }
                    
                    _completeCallback();
                    _action = null;
                    _isRunning = false;
                }
            });
        }

        public void SetTask(Action action) => _action = action;

    }
}
