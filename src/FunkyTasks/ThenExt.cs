using System;
using System.Threading.Tasks;
using System.Threading;

namespace FunkyTasks {
    public static class ThenExt {
        public static Task<B> Then<A,B> (
            this Task<A> task,
            Action<A,TaskCompletionSource<B>> then,
            TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            return task.Control <A,B> ((t, tcs) => {
                if (t.IsFaulted) {
                    tcs.SetException (t.Exception);
                    return;
                }
                if (t.IsCanceled) {
                    tcs.SetCanceled ();
                    return;
                }
                try {
                    then (t.Result, tcs);
                } catch (Exception ex) {
                    tcs.SetException (ex);
                }
            }, scheduler, cancel);
        }

        public static Task<B> Then<A,B> (
            this Task<A> task,
            Func<A, B> f,
            TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            return task.Then <A,B> ((a, tcs) => {
                var res = f (a);
                tcs.SetResult (res);
            }, scheduler, cancel);
        }
    }
}
