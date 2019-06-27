namespace JobProcessor.JobProcessLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JobProcessor.SortLib;

    public class JobSequencer
    {
        private readonly List<Job> _jobContainer;

        public JobSequencer(List<Job> jobContainer)
        {
            this._jobContainer = jobContainer;
        }

        public string GetSequencedJobs()
        {
            var listSortedJobs = new Sorter().GetSOrtedJobs(this._jobContainer);

            // output sequence logic
            var sequencedJobs = String.Join("", listSortedJobs.Select(j => j.GetName()));

            return sequencedJobs;
        }
    }
}
