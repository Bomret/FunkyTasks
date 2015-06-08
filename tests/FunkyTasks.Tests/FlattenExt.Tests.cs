using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace FunkyTasks.Tests {
    [TestFixture]
    public class When_a_nested_task_is_flattened : TestBase {
        [Test]
        public void it_should_return_the_faulted_inner_task_for_a_successful_task() {
            var err = new ArgumentException("Expected err");

            Ok(Err<int>(err))
                .Flatten(TestScheduler)
                .Exception
                .Flatten()
                .InnerExceptions
                .Single()
                .Should()
                .Be(err);
        }

        [Test]
        public void it_should_return_the_original_cancelled_task() {
            Cancel<Task<int>>()
                .Flatten(TestScheduler)
                .IsCanceled
                .Should()
                .BeTrue();
        }

        [Test]
        public void it_should_return_the_original_faulted_task() {
            var err = new ArgumentException("Expected err");

            Err<Task<int>>(err)
                .Flatten(TestScheduler)
                .Exception
                .Flatten()
                .InnerExceptions
                .Single()
                .Should()
                .Be(err);
        }

        [Test]
        public void it_should_return_the_successful_inner_task_for_a_successful_task() {
            Ok(Ok(3))
                .Flatten(TestScheduler)
                .Result
                .Should()
                .Be(3);
        }
    }
}