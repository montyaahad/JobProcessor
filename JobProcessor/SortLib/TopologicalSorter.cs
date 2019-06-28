using System.Collections.Generic;

namespace JobProcessor.SortLib
{
    using System;

    using JobProcessor.JobProcessLib;

    public class TopologicalSorter
    {
        public List<Job> GetSOrtedJobs(List<Job> jobContainer)
        {
            var sortedJobs = new List<Job>();
            var visitedJobs = new Dictionary<Job, bool>();

            foreach (var job in jobContainer)
            {
                this.VisitJob(job, sortedJobs, visitedJobs);
            }

            return sortedJobs;
        }

        private void VisitJob(Job item, List<Job> sorted, Dictionary<Job, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new Exception("jobs can’t have circular dependencies");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = item.GetDependencyList();
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        this.VisitJob(dependency, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
