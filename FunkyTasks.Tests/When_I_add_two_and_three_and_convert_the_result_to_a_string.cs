using System;
using System.Threading.Tasks;
using Machine.Specifications;

namespace FunkyTasks.Tests
{
    [Subject(typeof (Combinators), "Map")]
    public class When_I_add_two_and_three_and_convert_the_result_to_a_string
    {
        static Func<int> _addTwoAndThree;
        static Func<int, string> _toString;
        static Task<string> _result;

        Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;

            _toString = i => i.ToString();
        };

        Because of = () => _result = Task.Factory.StartNew(_addTwoAndThree)
                                         .Map(_toString);

        It should_return_five_as_string_in_the_task = () => _result.Result.ShouldEqual("5");
    }
}