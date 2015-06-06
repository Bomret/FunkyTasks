using System;
using System.Threading.Tasks;
using System.Threading;

namespace FunkyTasks {

    public static class SelectExt {
        public static Task<B> Select<A,B> (this Task<A> task, Func<A,B> select, TaskScheduler scheduler, CancellationToken cancel = default(CancellationToken)) {
            return task.Then (t => select(t),scheduler,cancel);
        }

        public static Task<B> Select<A,B> (this Task<A> task, Func<A,B> select, CancellationToken cancel = default(CancellationToken)){
            return Select (task, select, TaskScheduler.Default, cancel);
        }
    }

}
