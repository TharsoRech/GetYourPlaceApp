namespace GetYourPlaceApp.Services.BackGroundTask
{
    public enum BackgroundTaskStatus
    {
        none,
        Running,
        Completed,
        Failed
    }

    public interface IBackgroundTaskRunner<T>
    {
        event EventHandler<BackgroundTaskEventArgs<T>> StatusChanged;

        void RunInBackground(Func<Task<T>> backGroundTask);    
    }

    public class BackgroundTaskEventArgs<T>
    {
        public BackgroundTaskEventArgs(
            BackgroundTaskStatus backgroundTaskStatus,
            T result,Exception error)
        {
            TaskStatus = backgroundTaskStatus;
            Result = result;
            Error = error;
        }

        public BackgroundTaskStatus TaskStatus { set; get; } = BackgroundTaskStatus.none;

        public T Result { set; get; }

        public Exception Error { set; get; }
    }

    public class BackgroundTaskRunner<T> : IBackgroundTaskRunner<T>, IDisposable
    {
        public event EventHandler<BackgroundTaskEventArgs<T>> StatusChanged;
        CancellationTokenSource _cancellationTokenSource;
        Task<Task> _runnableTask;
        public bool HasDisposed;


        public void RunInBackground(Func<Task<T>> backGroundTask)
        {
            if (_runnableTask is null || _runnableTask.IsCompleted)
                Dispose();

            _cancellationTokenSource = new CancellationTokenSource(10000);
            _runnableTask = Task.Factory.StartNew(async () =>
            {
                try
                {
                    OnStatusChanged(BackgroundTaskStatus.Running, default,_cancellationTokenSource,null);  
                    var result = await backGroundTask();
                    OnStatusChanged(BackgroundTaskStatus.Completed, result, _cancellationTokenSource, null);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    OnStatusChanged(BackgroundTaskStatus.Completed, default, _cancellationTokenSource, ex);
                }
            }, _cancellationTokenSource.Token);
        }

        private void OnStatusChanged(BackgroundTaskStatus
            backgroundTaskStatus,
            T result,
            CancellationTokenSource cancellationTokenSource, Exception exception)
        {
            if(_runnableTask !=null && _cancellationTokenSource != null)
            {
                if(!_runnableTask.IsCanceled && _cancellationTokenSource.IsCancellationRequested)
                {
                    StatusChanged.Invoke(this,new BackgroundTaskEventArgs<T>(backgroundTaskStatus,result,exception));
                }
            }
        }

        public void Dispose()
        {
            try
            {
                HasDisposed = true;
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
                _runnableTask.Dispose();
                _cancellationTokenSource = null;
                _runnableTask = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
