using System;

namespace JobProcessor
{
    using JobProcessor.JobProcessLib;

    class Program
    {
        static void Main(string[] args)
        {
            // sample input
            string input = "a=> | b=>c |c=>c";

            var jobManger = new JobManager();
            var result = jobManger.GetSortedJobs(input);

            Console.WriteLine(result);

            Console.Read();
        }
    }
}
