using System.Threading;
using System.Threading.Tasks;

namespace FunkyTasks {
    public static class OrElseExt {
        public static Task<T> OrElse<T>(this Task<T> task, T fallback,
            CancellationToken cancel = default(CancellationToken)) {
            return OrElse(task, fallback, TaskScheduler.Default, cancel);
        }

        public static Task<T> OrElse<T>(this Task<T> task, T fallback, TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            return task.Control<T, T>((t, tcs) => {
                if (t.IsFaulted || t.IsCanceled)
                    tcs.SetResult(fallback);
                else
                    tcs.SetResult(t.Result);
            }, scheduler, cancel);
        }
    }
}