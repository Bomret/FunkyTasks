using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FunkyTasks.Tests {
    [TestFixture]
    public class When_a_function_is_applied_to_a_task : TaskTests {
        [Test]
        public void it_should_return_the_result_after_applying_the_function_for_a_successful_task () {
            var task = Ok (3).Select (i => i + 2, _scheduler);
            Assert.AreEqual (5, task.Result);
        }

        [Test]
        public void it_should_return_an_error_after_applying_the_function_for_an_unsuccessful_task () {
            var err = new ArgumentException ("Expected err");
            var task = Fail<int> (err).Select (i => i + 2, _scheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.First ());
        }

        [Test]
        public void it_should_return_a_cancelled_task_after_applying_the_function_for_a_cancelled_task () {
            var task = Cancelled<int> ().Select (i => i + 2, _scheduler);
            Assert.IsTrue (task.IsCanceled);
        }
    }
}

