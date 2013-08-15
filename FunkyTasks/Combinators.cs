using System;
using System.Threading.Tasks;

namespace FunkyTasks
{
    public static class Combinators
    {
        public static Task<TNewValue> Map<TValue, TNewValue>(this Task<TValue> task,
                                                             Func<TValue, TNewValue> map)
        {
            return task.ContinueWith(t => map(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public static Task<TValue> OrElse<TValue>(this Task<TValue> task,
                                                  Func<TValue> orElse)
        {
            return task.ContinueWith(t => t.IsFaulted
                                              ? orElse()
                                              : t.Result);
        }
    }
}