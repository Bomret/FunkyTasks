using System;
using System.Threading.Tasks;
using Machine.Specifications;

namespace FunkyTasks.Tests
{
    [Subject(typeof (Combinators), "OrElse")]
    public class When_I_divide_by_zero_and_add_one_and_three_instead
    {
        static Task<int> _result;
        static Func<int> _addOneAndThree;
        static Func<int> _divideByZero;

        Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;
                return 5 / zero;
            };
            _addOneAndThree = () => 1 + 3;
        };

        Because of = () => _result = Task.Factory.StartNew(_divideByZero)
                                         .OrElse(_addOneAndThree);

        It should_return_four_in_the_task =
            () => _result.Result.ShouldEqual(4);
    }
}