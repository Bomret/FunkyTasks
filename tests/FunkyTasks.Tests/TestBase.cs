using System;
using System.Threading.Tasks;

namespace FunkyTasks.Tests {
    public abstract class TestBase {
        protected static TaskScheduler TestScheduler = new CurrentThreadTaskScheduler();

        public static Task<T> Err<T>(Exception err) {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetException(err);

            return tcs.Task;
        }

        public static Task<T> Ok<T>(T val) {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(val);

            return tcs.Task;
        }

        public static Task<T> Cancel<T>() {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetCanceled();

            return tcs.Task;
        }
    }
}