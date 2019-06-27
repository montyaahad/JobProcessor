namespace JobProcessor.JobProcessLib
{
    using System;
    using System.Collections.Generic;

    public class Job
    {
        private string _name;
        private List<Job> _dependencyList;

        public void Initiate(string name)
        {
            this._name = name;
            this._dependencyList = new List<Job>();
        }

        public string GetName()
        {
            return this._name;
        }

        public void AddDependency(Job job)
        {
            if (job.GetName().Equals(this._name))
            {
                throw new Exception("jobs can’t depend on themselves");
            }

            this._dependencyList.Add(job);
        }

        public List<Job> GetDependencyList()
        {
            return this._dependencyList;
        }
    }
}
