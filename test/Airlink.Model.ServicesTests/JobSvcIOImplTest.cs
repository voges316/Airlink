using Airlink.Model.Domain;
using Airlink.Model.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Airlink.Model.ServicesTests
{
    [TestClass]
    public class JobSvcIOImplTest
    {
        // The service to use
        IJobSvc jobImpl = new JobSvcIOImpl();

        [TestMethod]
        public void TestSaveDeleteJob()
        {
            Job exec = new Job("exec");
            Assert.IsTrue(jobImpl.SaveJob(exec));

            // False since it's now already in db
            Assert.IsFalse(jobImpl.SaveJob(exec));

            // Cleanup & test delete
            Assert.IsTrue(jobImpl.DeleteJob(exec.Name));
        }

        [TestMethod]
        public void TestGetJob()
        {
            Job exec = new Job("exec");
            jobImpl.SaveJob(exec);

            Job result = jobImpl.GetJob(exec.Name);
            Assert.AreEqual(exec, result);

            // Cleanup
            jobImpl.DeleteJob(exec.Name);
        }

        [TestMethod]
        public void TestUpdateJob()
        {
            Job exec = new Job("exec");

            // False because not in db yet
            Assert.IsFalse(jobImpl.UpdateJob(exec));

            jobImpl.SaveJob(exec);
            Job exec2 = new Job("exec");
            Assert.IsTrue(jobImpl.UpdateJob(exec2));

            //Cleanup
            jobImpl.DeleteJob(exec2.Name);
        }

        [TestMethod]
        public void TestGetAllJobs()
        {
            Job exec = new Job("exec");
            Job ado = new Job("ado");
            jobImpl.SaveJob(exec);
            jobImpl.SaveJob(ado);

            // Returned list should contain 2 jobs
            IList<Job> jobs = jobImpl.GetAllJobs();
            Assert.IsTrue(jobs.Count == 2);

            // Cleanup
            jobImpl.DeleteJob(exec.Name);
            jobImpl.DeleteJob(ado.Name);
        }
    }
}
