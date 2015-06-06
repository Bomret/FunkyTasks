using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunkyTasks.Tests {
    public abstract class TaskTests {
        protected static TaskScheduler _scheduler = new CurrentThreadTaskScheduler ();

        public static Task<T> Fail<T>(Exception err){
            var tcs = new TaskCompletionSource<T> ();
            tcs.SetException (err);

            return tcs.Task;
        }

        public static Task<T> Ok<T>(T val){
            var tcs = new TaskCompletionSource<T> ();
            tcs.SetResult (val);

            return tcs.Task;
        }

        public static Task<T> Cancelled<T>(){
            var tcs = new TaskCompletionSource<T> ();
            tcs.SetCanceled ();

            return tcs.Task;
        }
    }
    
}
