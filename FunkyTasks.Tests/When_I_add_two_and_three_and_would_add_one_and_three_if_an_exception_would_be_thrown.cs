using System;
using System.Threading.Tasks;
using Machine.Specifications;

namespace FunkyTasks.Tests
{
    [Subject(typeof (Combinators), "OrElse")]
    public class When_I_add_two_and_three_and_would_add_one_and_three_if_an_exception_would_be_thrown
    {
        static Func<int> _addTwoAndThree;
        static Task<int> _result;
        static Func<int> _addOneAndThree;

        Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _addOneAndThree = () => 1 + 3;
        };

        Because of = () => _result = Task.Factory.StartNew(_addTwoAndThree)
                                         .OrElse(_addOneAndThree);

        It should_return_five_in_the_task =
            () => _result.Result.ShouldEqual(5);
    }
}