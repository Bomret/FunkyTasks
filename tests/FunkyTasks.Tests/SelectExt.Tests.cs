using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace FunkyTasks.Tests {
    [TestFixture]
    public class When_a_function_is_applied_to_a_task_using_select : TestBase {
        [Test]
        public void it_should_return_an_error_after_applying_a_faulty_function_for_a_successful_task() {
            var err = new ArgumentException("Expected err");

            Ok(3)
                .Select<int, int>(_ => { throw err; }, TestScheduler)
                .Exception
                .Flatten()
                .InnerExceptions
                .Single()
                .Should()
                .Be(err);
        }

        [Test]
        public void it_should_return_an_error_and_not_apply_the_function_for_an_unsuccessful_task() {
            var err = new ArgumentException("Expected err");

            Err<int>(err)
                .Select(i => i + 2, TestScheduler)
                .Exception
                .Flatten()
                .InnerExceptions
                .Single()
                .Should()
                .Be(err);
        }

        [Test]
        public void it_should_return_the_cancelled_task_and_not_apply_the_faulty_function_for_a_cancelled_task() {
            var err = new ArgumentException("Expected err");

            Cancel<int>()
                .Select<int, int>(_ => { throw err; }, TestScheduler)
                .IsCanceled
                .Should()
                .BeTrue();
        }

        [Test]
        public void it_should_return_the_cancelled_task_and_not_apply_the_function_for_a_cancelled_task() {
            Cancel<int>()
                .Select(i => i + 2, TestScheduler)
                .IsCanceled
                .Should()
                .BeTrue();
        }

        [Test]
        public void it_should_return_the_original_error_and_not_apply_the_faulty_function_for_an_unsuccessful_task() {
            var originalErr = new ArgumentException("Expected err");
            var err = new ArgumentException("Expected err");

            Err<int>(originalErr)
                .Select<int, int>(_ => { throw err; }, TestScheduler)
                .Exception.Flatten().InnerExceptions.Single()
                .Should()
                .Be(originalErr);
        }

        [Test]
        public void it_should_return_the_result_after_applying_the_function_for_a_successful_task() {
            Ok(3)
                .Select(i => i + 2, TestScheduler)
                .Result.Should().Be(5);
        }
    }
}