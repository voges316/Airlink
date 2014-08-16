using Airlink.Model.Domain;
using Airlink.Model.Services;
using System;
using System.Collections.Generic;

namespace Airlink.Model.Business
{
    public class JobManager : ManagerSupertype
    {
        // Thread safe singleton
        private static readonly JobManager instance = new JobManager();

        static JobManager() { }

        private JobManager() { }

        public static JobManager Instance
        {
            get
            {
                Console.WriteLine("Making an instance of Job Manager");
                return instance;
            }
        }


        // Take job obj and returns false if not save or exceptions are thrown
        public bool SaveJob(Job job)
        {
            bool result = false;
            IJobSvc jobSvc;
            try
            {
                jobSvc = (IJobSvc)GetService(typeof(IJobSvc).Name);
                result = jobSvc.SaveJob(job);
            }
            catch (Exception e)
            {
                Console.WriteLine("JobManager had an error saving a job: {0}", e);
            }

            return result;
        }

        // Returns job obj that matches string name, and null otherwise or if an exception is thrown
        public Job GetJob(string jobName)
        {
            Job result = null;
            IJobSvc jobSvc;
            try
            {
                jobSvc = (IJobSvc)GetService(typeof(IJobSvc).Name);
                result = jobSvc.GetJob(jobName);
            }
            catch (Exception e)
            {
                Console.WriteLine("JobManager had an error finding a job: {0}", e);
            }

            return result;
        }

        // Returns true if job is in database and is updated. False if not in db or exception thrown
        public bool UpdateJob(Job job)
        {
            bool result = false;
            IJobSvc jobSvc;
            try
            {
                jobSvc = (IJobSvc)GetService(typeof(IJobSvc).Name);
                result = jobSvc.UpdateJob(job);
            }
            catch (Exception e)
            {
                Console.WriteLine("JobManager had an error updating a job: {0}", e);
            }

            return result;
        }

        // Deletes job in db based on string name. False if not found or error thrown
        public bool DeleteJob(string jobName)
        {
            bool result = false;
            IJobSvc jobSvc;
            try
            {
                jobSvc = (IJobSvc)GetService(typeof(IJobSvc).Name);
                result = jobSvc.DeleteJob(jobName);
            }
            catch (Exception e)
            {
                Console.WriteLine("JobManager had an error deleting a job: {0}", e);
            }

            return result;
        }

        // Returns IList of all jobs in db
        public IList<Job> GetAllJobs()
        {
            IList<Job> result;
            IJobSvc jobSvc;
            try
            {
                jobSvc = (IJobSvc)GetService(typeof(IJobSvc).Name);
                result = jobSvc.GetAllJobs();
                // return IList
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("JobManager had an error getting a list of jobs: {0}", e);
            }

            return result = new List<Job>();
        }

    }
}
