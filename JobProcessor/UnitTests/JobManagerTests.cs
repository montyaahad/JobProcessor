using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobProcessor.UnitTests
{
    using JobProcessor.JobProcessLib;

    [TestClass]
    public class JobManagerTests
    {
        [TestMethod]
        public void EmptyStringAsInputShouldReturnEmptySequence()
        {
            string input = " ";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void SingleJobAsInputShouldReturnSingleSequence()
        {
            string input = "a=>";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void InputValidation()
        {
            // TODO
            string input = "a";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void JobsWithNoDependencyAsInputShouldReturnAllJobsWithNoSpecificSequence()
        {
            string input = "a=>|b=>";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);

            var expected = new List<string>() { "ab", "ba" };

            Assert.IsTrue(expected.Contains(result));
        }

        [TestMethod]
        public void JobsWithDependencyAsInputShouldReturnAllJobsWithSequence()
        {
            string input = "a=>|b=>c|c=>";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);

            var expected = new List<string>() { "acb", "cba" };

            Assert.IsTrue(expected.Contains(result));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "jobs can’t depend on themselves")]
        public void JobsWithSelfDependencyAsInputShouldThrowException()
        {
            string input = "a=>|b=>b|c=>";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "jobs can’t have circular dependencies")]
        public void JobsWithCircularDependencyAsInputShouldThrowException()
        {
            string input = "a=>|b=>c|c=>b";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);
        }
    }
}
