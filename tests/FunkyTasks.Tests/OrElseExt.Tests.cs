using System;
using FluentAssertions;
using NUnit.Framework;

namespace FunkyTasks.Tests {
    [TestFixture]
    public class When_a_fallback_value_is_used_for_a_faulted_or_cancelled_task : TestBase {
        [Test]
        public void it_should_return_the_fallback_value_for_a_cancelled_task() {
            Cancel<int>()
                .OrElse(5, TestScheduler)
                .Result
                .Should()
                .Be(5);
        }

        [Test]
        public void it_should_return_the_fallback_value_for_a_faulted_task() {
            var err = new ArgumentException("Expected err");

            Err<int>(err)
                .OrElse(5, TestScheduler)
                .Result
                .Should()
                .Be(5);
        }

        [Test]
        public void it_should_return_the_initial_value_for_a_successful_task() {
            Ok(3)
                .OrElse(5, TestScheduler)
                .Result
                .Should()
                .Be(3);
        }
    }
}