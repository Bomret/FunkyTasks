using System;
using System.Threading.Tasks;
using Machine.Specifications;

namespace FunkyTasks.Tests
{
    [Subject(typeof (Combinators))]
    public class When_I_divide_by_zero_and_convert_the_result_to_a_string
    {
        private static Func<int> _divideByZero;
        private static Func<int, string> _toString;
        private static Task<string> _result;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;
                return 5/zero;
            };

            _toString = i => i.ToString();
        };

        private Because of = () => _result = Task.Factory.StartNew(_divideByZero)
                                                 .Map(_toString);

        private It should_contain_a_DivideByZeroException_in_the_task =
            () =>
            {
                Task.WaitAll(new[] {_result});
                _result.Exception.GetBaseException().ShouldBeOfType<DivideByZeroException>();
            };
    }
}