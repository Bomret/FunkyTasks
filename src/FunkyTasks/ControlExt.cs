using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunkyTasks {
    public static class ControlExt {
        public static Task<B> Control<A, B>(
            this Task<A> task,
            Action<Task<A>, TaskCompletionSource<B>> control,
            TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            var tcs = new TaskCompletionSource<B>();

            task.ContinueWith(t => control(t, tcs), cancel, TaskContinuationOptions.None, scheduler);

            return tcs.Task;
        }
    }
}