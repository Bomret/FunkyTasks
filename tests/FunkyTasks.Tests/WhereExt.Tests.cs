using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FunkyTasks.Tests {

    [TestFixture]
    public class When_a_predicate_is_applied_to_a_task_using_where : TestBase {
        [Test]
        public void it_should_return_the_result_after_applying_the_predicate_for_a_successful_task () {
            var task = Ok (3).Where (i => i > 2, TestScheduler);

            Assert.AreEqual (3, task.Result);
        }

        [Test]
        public void it_should_return_an_error_after_applying_a_failing_predicate_for_a_successful_task () {
            var task = Ok (3).Where (i => i < 2, TestScheduler);

            Assert.AreEqual (
                typeof(PredicateFailedException),
                task.Exception.Flatten ().InnerExceptions.Single ().GetType ());
        }

        [Test]
        public void it_should_return_an_error_after_applying_a_faulty_predicate_for_a_successful_task () {
            var err = new ArgumentException ("Expected err");

            var task = Ok (3).Where<int> (_ => {
                throw err;
            }, TestScheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_an_error_and_not_apply_the_predicate_for_an_unsuccessful_task () {
            var err = new ArgumentException ("Expected err");

            var task = Err<int> (err).Where (i => i > 2, TestScheduler);

            Assert.AreEqual (err, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_the_original_error_and_not_apply_the_faulty_predicate_for_an_unsuccessful_task () {
            var originalErr = new ArgumentException ("Expected err");

            var task = Err<int> (originalErr).Where<int> (i => {
                throw new Exception ();
            }, TestScheduler);

            Assert.AreEqual (originalErr, task.Exception.Flatten ().InnerExceptions.Single ());
        }

        [Test]
        public void it_should_return_the_cancelled_task_and_not_apply_the_predicate_for_a_cancelled_task () {
            var task = Cancel<int> ().Where (i => i > 2, TestScheduler);

            Assert.IsTrue (task.IsCanceled);
        }

        [Test]
        public void it_should_return_the_cancelled_task_and_not_apply_the_faulty_predicate_for_a_cancelled_task () {
            var task = Cancel<int> ().Where<int> (i => {
                throw new Exception ();
            }, TestScheduler);

            Assert.IsTrue (task.IsCanceled);
        }
    }
}
