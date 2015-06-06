using System;
using System.Threading.Tasks;
using System.Threading;

namespace FunkyTasks {

    public static class WhereExt {
        public static Task<T> Where<T> (
            this Task<T> task,
            Func<T,bool> predicate,
            TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            return task.Then<T,T> ((t, tcs) => {
                if (predicate (t)) {
                    tcs.SetResult (t);   
                } else {
                    tcs.SetException (new PredicateFailedException ("The predicate did not hold for the result of the given Task."));
                }
            }, scheduler, cancel);
        }

        public static Task<T> Where<T> (
            this Task<T> task,
            Func<T,bool> predicate,
            CancellationToken cancel = default(CancellationToken)) {
            return Where (task, predicate, TaskScheduler.Default, cancel);
        }
    }

}
