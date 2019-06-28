namespace JobProcessor.JobProcessLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class JobManager
    {
        private string _input;
        private List<Job> _jobContainer = new List<Job>();
        private string _output;

        public string GetSortedJobs(string input)
        {
            if (string.IsNullOrEmpty(input.Trim()))
            {
                return string.Empty;
            }

            this._input = input.Trim();

            this.ValidateInput();

            this.ProcessJobs();

            return this._output;
        }

        private void ProcessJobs()
        {
            this.CreateJobContainer();

            this.SequenceJobs();
        }

        private void SequenceJobs()
        {
            var jobSequencer = new JobSequencer(this._jobContainer);
            this._output = jobSequencer.GetSequencedJobs();
        }

        private void CreateJobContainer()
        {
            var jobs = this._input.Trim(' ').Split('|');

            foreach (var job in jobs)
            {
                var parsedJob = this.ParseSingleJobInput(job);
                this.AddJob(parsedJob);
            }
        }

        private SingleJobSequence ParseSingleJobInput(string input)
        {
            var sjob = new SingleJobSequence(input);
            return sjob;
        }

        private void AddJob(SingleJobSequence jobUnit)
        {
            if (this.IsJobExists(jobUnit.GetJobName()))
            {
                var index = this.GetJobIndex(jobUnit.GetJobName());
                this.CreateJobDependency(jobUnit, index);
            }
            else
            {
                this.CreateJob(jobUnit.GetJobName());

                var index = this._jobContainer.Count - 1;
                this.CreateJobDependency(jobUnit, index);
            }
        }

        private int GetJobIndex(string jobName)
        {
            return this._jobContainer.FindIndex(j => j.GetName().Equals(jobName));
        }

        private void CreateJobDependency(SingleJobSequence jobUnit, int jobIndex)
        {
            if (string.IsNullOrEmpty(jobUnit.GetDependsOnName()))
            {
                return;
            }

            var indexOdDependency = this.GetJobIndex(jobUnit.GetDependsOnName());

            var job = this.IsJobExists(jobUnit.GetDependsOnName()) ? this._jobContainer[indexOdDependency] : this.CreateJob(jobUnit.GetDependsOnName());

            this._jobContainer[jobIndex].AddDependency(job);
        }

        private Job CreateJob(string jobName)
        {
            var newJob = new Job();
            newJob.Initiate(jobName);
            this._jobContainer.Add(newJob);

            return newJob;
        }

        private bool IsJobExists(string jobName)
        {
            return this._jobContainer.Any(j => j.GetName().Equals(jobName));
        }

        private void ValidateInput()
        {
            // sample valid inputs
            // a=>
            // a=>b
            // a=>|b=>|c=>b
            string pattern = @"([a-z]{1})(=>)(([a-z]{1}?))?((\|)?(([a-z]{1})(=>)(([a-z]{1}?))?)?)*";

            Match match = Regex.Match(this._input, pattern, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new Exception("invalid input format");
            }
        }
    }
}
