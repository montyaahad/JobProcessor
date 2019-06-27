using System.Collections.Generic;

namespace JobProcessor.SortLib
{
    using JobProcessor.JobProcessLib;

    public class Sorter
    {
        public List<Job> GetSOrtedJobs(List<Job> jobContainer)
        {
            // TODO : implement sorting algorithm based on dependency

            // for now just returning the same list

            return jobContainer;
        }
    }
}
