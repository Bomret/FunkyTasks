using System.Threading.Tasks;
using System.Threading;

namespace FunkyTasks {
    public static class FlattenExt {
        public static Task<T> Flatten<T> (this Task<Task<T>> nestedTask, TaskScheduler scheduler, CancellationToken cancel = default(CancellationToken)) {
            return nestedTask.Then<Task<T>,T> ((task, tcs) => tcs.SetResult (task.Result), scheduler, cancel);
        }

        public static Task<T> Flatten<T> (this Task<Task<T>> nestedTask, CancellationToken cancel = default(CancellationToken)) {
            return Flatten (nestedTask, TaskScheduler.Default, cancel);
        }
    }

}
