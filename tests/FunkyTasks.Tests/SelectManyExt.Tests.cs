using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FunkyTasks.Tests {
    [TestFixture]
    public class When_a_function_is_applied_to_a_task_using_select_many : TestBase {
        [Test]
        public void it_should_return_the_result_after_applying_the_function_for_a_successful_task () {
            var task = Ok (3).SelectMany (i => Task.FromResult (i + 2), TestScheduler);

            Assert.AreEqual (5, task.Result);
        }

        [Test]
        public void it_should_return_an_error_after_applying_a_faulty_function_for_a_successful_task () {
            var err = new ArgumentException ("Expected err");

            var task = Ok (3).SelectMany<int,int> (_ => {
                throw err;
            }, TestScheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_an_error_and_not_apply_the_function_for_an_unsuccessful_task () {
            var err = new ArgumentException ("Expected err");

            var task = Err<int> (err).SelectMany (i => Task.FromResult (i + 2), TestScheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_the_original_error_and_not_apply_the_faulty_function_for_an_unsuccessful_task () {
            var originalErr = new ArgumentException ("Expected err");
            var err = new ArgumentException ("Expected err");

            var task = Err<int> (originalErr).SelectMany<int,int> (_ => {
                throw err;
            }, TestScheduler);

            Assert.AreEqual (originalErr, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_the_cancelled_task_and_not_apply_the_function_for_a_cancelled_task () {
            var task = Cancel<int> ().SelectMany (i => Task.FromResult (i + 2), TestScheduler);

            Assert.IsTrue (task.IsCanceled);
        }

        [Test]
        public void it_should_return_the_cancelled_task_and_not_apply_the_faulty_function_for_a_cancelled_task () {
            var err = new ArgumentException ("Expected err");

            var task = Cancel<int> ().SelectMany<int,int> (_ => {
                throw err;
            }, TestScheduler);

            Assert.IsTrue (task.IsCanceled);
        }
    }
}
