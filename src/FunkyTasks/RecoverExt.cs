using System.Threading.Tasks;
using System.Threading;
using System;

namespace FunkyTasks {
	public static class RecoverExt
	{
		public static Task<A> Recover<A>(this Task<A> task, Func<Exception,A> recover, TaskScheduler scheduler, CancellationToken candel = default(CancellationToken)) {
			return task.Control<A,A>((t, tcs)=> {
                if (t.IsCanceled) {
                    tcs.SetCanceled ();
                }
                else if (t.IsFaulted) {
                    try {
                        var next = recover (t.Exception);
                        tcs.SetResult (next);
                    } catch (Exception err) {
                        tcs.SetException (err);
                    }
                    return;
                } else {
                    tcs.SetResult (t.Result);
                }
            }, scheduler, candel);
		}
	}
}