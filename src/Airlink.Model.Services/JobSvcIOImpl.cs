using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airlink.Model.Services
{
    public class JobSvcIOImpl : IJobSvc
    {
        // Saves job and returns true. Returns false if already exists
        public bool SaveJob(Job job)
        {
            bool result = false;
            string name = job.Name;
            try
            {
                DbCollections.SquadronJobs.Add(name, job);
                result = true;                
            }
            catch (ArgumentException)
            {
                Console.WriteLine("An element with key {0} already exists", name);
            }
            return result;
        }

        // Retrieves job based on name. Returns null if not found
        public Job GetJob(string jobName)
        {
            Job job = null;
            DbCollections.SquadronJobs.TryGetValue(jobName, out job); 

            return job;
        }

        // If job name exists overwrites it with new job info and returns true. false if not found
        public bool UpdateJob(Job job)
        {
            bool result = false;
            if (DbCollections.SquadronJobs.ContainsKey(job.Name))
            {
                DbCollections.SquadronJobs[job.Name] = job;
                result = true;
            }

            return result;
        }

        // true if element found and removed. false if key is not found
        public bool DeleteJob(string jobName)
        {
            // Check for employee dependencies using similar approach used in DeletePerson
            Job job = GetJob(jobName);
            if (job != null && job.Employees.Count > 0)
            {
                IPersonnelSvc pServ = new PersonnelSvcIOImpl();
                foreach (Employee emp in job.Employees)
                {
                    Employee e = pServ.GetEmployee(emp.FirstName, emp.LastName);
                    e.Jobs.Remove(job);
                    pServ.UpdatePerson(e);
                }
            }

            return DbCollections.SquadronJobs.Remove(jobName);
        }

        public IList<Job> GetAllJobs()
        {
            return DbCollections.SquadronJobs.Values.ToList();
        }
    }
}
