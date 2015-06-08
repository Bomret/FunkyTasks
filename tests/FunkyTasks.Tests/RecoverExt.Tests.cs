using System;
using FluentAssertions;
using NUnit.Framework;

namespace FunkyTasks.Tests {
	[TestFixture]
	public sealed class When_a_task_is_recovered_from_faulting : TestBase
	{
		[Test]
		public void it_should_return_the_original_result_for_a_successful_task() {
			Ok(3)
                .Recover(err => 5, TestScheduler)
                .Result
                .Should().Be(3);
		}
		
		[Test]
		public void it_should_return_the_new_result_for_a_faulted_task() {
			Err<int>(new Exception("test exception"))
                .Recover(err => 5, TestScheduler)
                .Result
                .Should().Be(5);
		}
		
		[Test]
		public void it_should_return_a_cancelled_task_for_a_cancelled_task() {
			Cancel<int>()
                .Recover(err => 5, TestScheduler)
                .IsCanceled
                .Should().BeTrue();
		}
	}	
}