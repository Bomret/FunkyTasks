using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunkyTasks {
    public static class SelectManyExt {
        public static Task<C> SelectMany<A, B, C>(
            this Task<A> task,
            Func<A, Task<B>> taskSelect,
            Func<A, B, C> resultSelect,
            TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            return task
                .Then(a => taskSelect(a).Then(b => resultSelect(a, b), scheduler, cancel), scheduler, cancel)
                .Then(tc => tc.Result, scheduler, cancel);
        }

        public static Task<C> SelectMany<A, B, C>(
            this Task<A> task,
            Func<A, Task<B>> taskSelect,
            Func<A, B, C> resultSelect,
            CancellationToken cancel = default(CancellationToken)) {
            return SelectMany(task, taskSelect, resultSelect, TaskScheduler.Default, cancel);
        }

        public static Task<B> SelectMany<A, B>(
            this Task<A> task,
            Func<A, Task<B>> select,
            TaskScheduler scheduler,
            CancellationToken cancel = default(CancellationToken)) {
            return task
                .Then(@select, scheduler, cancel)
                .Then(b => b.Result, scheduler, cancel);
        }

        public static Task<B> SelectMany<A, B>(
            this Task<A> task,
            Func<A, Task<B>> select,
            CancellationToken cancel = default(CancellationToken)) {
            return SelectMany(task, select, TaskScheduler.Default, cancel);
        }
    }
}