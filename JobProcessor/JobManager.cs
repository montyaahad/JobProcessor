using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobProcessor
{
    public class JobManager
    {
        private string _input;
        private List<Job> _jobContainer = new List<Job>();
        private string _output;

        public string GetSortedJobs(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            _input = input;

            CheckInput();

            ProcessJobs();

            return _output;
        }

        private void ProcessJobs()
        {
            CreateJobContainer();

            SequenceJobs();
        }

        private void SequenceJobs()
        {
            // TODO: implement sorting

            this._output = String.Join(",", this._jobContainer.Select(j => j.GetName()));
        }

        private void CreateJobContainer()
        {
            var jobs = this._input.Trim(' ').Split('|');

            foreach (var job in jobs)
            {
                var parsedJob = ParseSingleJobInput(job);
                AddJob(parsedJob);
            }
        }

        private SingleJobSequence ParseSingleJobInput(string input)
        {
            var sjob = new SingleJobSequence(input);
            return sjob;
        }

        private void AddJob(SingleJobSequence jobUnit)
        {
            if (IsJobExists(jobUnit.GetJobName()))
            {
                var index = GetJobIndex(jobUnit.GetJobName());
                CreateJobDependency(jobUnit, index);
            }
            else
            {
                CreateJob(jobUnit.GetJobName());

                var index = this._jobContainer.Count - 1;
                CreateJobDependency(jobUnit, index);
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

            var indexOdDependency = GetJobIndex(jobUnit.GetDependsOnName());

            var job = IsJobExists(jobUnit.GetDependsOnName()) ? this._jobContainer[indexOdDependency] : CreateJob(jobUnit.GetDependsOnName());

            this._jobContainer[jobIndex].AddDependency(job);
        }

        private Job CreateJob(string jobName)
        {
            var newJob = new Job();
            newJob.Initiate(jobName);
            _jobContainer.Add(newJob);

            return newJob;
        }

        private bool IsJobExists(string jobName)
        {
            return this._jobContainer.Any(j => j.GetName().Equals(jobName));
        }

        private void CheckInput()
        {
            //var jobs = this._input.Trim(' ').Split('|');
        }
    }
}
