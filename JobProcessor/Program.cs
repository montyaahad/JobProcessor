using System;

namespace JobProcessor
{
    using JobProcessor.JobProcessLib;

    class Program
    {
        static void Main(string[] args)
        {
            // sample input
            //string input = "a=>|b=>|c=>b";

            Console.WriteLine("Please insert the input in correct format. "
                              + "\nUse '=>' for job dependency and bar (|) for separating jobs."
                              + "\n\nSome valid input formats given below - "
                              + "\n\ta=>"
                              + "\n\ta => | b => | c => b"
                              + "\n\ta=>|b=>|c=>b"
                              + "\n\nPlease enter your jobs below : ");

            string input = Console.ReadLine();

            var jobManger = new JobManager();

            try
            {
                var result = jobManger.GetSortedJobs(input);
                Console.WriteLine("\nSequenced Jobs : " + result);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError : " + e.Message);
            }
            
            Console.Read();
        }
    }
}
