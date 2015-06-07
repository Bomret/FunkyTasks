using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FunkyTasks.Tests {

    [TestFixture]
    public class When_a_fallback_value_is_used_for_a_faulted_or_cancelled_task : TestBase {
        [Test]
        public void it_should_return_the_initial_value_for_a_successful_task () {
            var task = Ok (3).OrElse (5, TestScheduler);

            Assert.AreEqual (3, task.Result);
        }

        [Test]
        public void it_should_return_the_fallback_value_for_a_faulted_task () {
            var err = new ArgumentException ("Expected err");

            var task = Err<int> (err).OrElse (5, TestScheduler);

            Assert.AreEqual (5, task.Result);
        }

        [Test]
        public void it_should_return_the_fallback_value_for_a_cancelled_task () {
            var task = Cancel<int> ().OrElse (5, TestScheduler);

            Assert.AreEqual (5, task.Result);
        }
    }
}
