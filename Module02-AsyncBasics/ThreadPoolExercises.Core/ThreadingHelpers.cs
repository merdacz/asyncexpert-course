using System;
using System.Threading;

namespace ThreadPoolExercises.Core
{
    public class ThreadingHelpers
    {
        public static void ExecuteOnThread(Action action, int repeats, CancellationToken token = default, Action<Exception>? errorAction = null)
        {
            var onError = errorAction ?? (_ => { });
            var t = new Thread(() => DoWork(action, repeats, token, onError));
            t.Start();
            t.Join();
        }

        public static void ExecuteOnThreadPool(Action action, int repeats, CancellationToken token = default, Action<Exception>? errorAction = null)
        {
            var onError = errorAction ?? (_ => { });
            var are = new AutoResetEvent(false);
            ThreadPool.QueueUserWorkItem(_ => DoWork(action, repeats, token, onError, are));
            are.WaitOne(1000);
        }

        public static void DoWork(Action action, int repeats, CancellationToken token, Action<Exception> onError, AutoResetEvent? are = null)
        {
            try
            {
                for (int i = 0; i < repeats; i++)
                {
                    token.ThrowIfCancellationRequested();
                    action();
                }
            }
            catch (Exception ex)
            {
                onError(ex);
            }
            finally
            {
                are?.Set();
            }
        }
    }
}
