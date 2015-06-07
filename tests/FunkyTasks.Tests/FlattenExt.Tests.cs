using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FunkyTasks.Tests {
    [TestFixture]
    public class When_a_nested_task_is_flattened : TestBase {
        [Test]
        public void it_should_return_the_successful_inner_task_for_a_successful_task () {
            var task = Ok (Ok (3)).Flatten (TestScheduler);

            Assert.AreEqual (3, task.Result);
        }

        [Test]
        public void it_should_return_the_faulted_inner_task_for_a_successful_task () {
            var err = new ArgumentException ("Expected err");

            var task = Ok (Err<int> (err)).Flatten (TestScheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_the_original_faulted_task () {
            var err = new ArgumentException ("Expected err");

            var task = Err<Task<int>> (err).Flatten (TestScheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_the_original_cancelled_task () {
            var task = Cancel<Task<int>> ().Flatten (TestScheduler);

            Assert.IsTrue (task.IsCanceled);
        }
    }
}
