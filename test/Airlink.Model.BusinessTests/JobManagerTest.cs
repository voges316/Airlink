using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airlink.Model.Business;
using Airlink.Model.Domain;
using System.Collections.Generic;

namespace Airlink.Model.BusinessTests
{
    [TestClass]
    public class JobManagerTest
    {
        JobManager jobMgr = JobManager.Instance;

        [TestMethod]
        public void TestSaveDeleteJob()
        {
            Job exec = new Job("exec");
            Assert.IsTrue(jobMgr.SaveJob(exec));

            // False since it's now already in db
            Assert.IsFalse(jobMgr.SaveJob(exec));

            // Cleanup & test delete
            Assert.IsTrue(jobMgr.DeleteJob(exec.Name));
        }

        [TestMethod]
        public void TestGetJob()
        {
            Job exec = new Job("exec");
            jobMgr.SaveJob(exec);

            Job result = jobMgr.GetJob(exec.Name);
            Assert.AreEqual(exec, result);

            // Cleanup
            jobMgr.DeleteJob(exec.Name);
        }

        [TestMethod]
        public void TestUpdateJob()
        {
            Job exec = new Job("exec");

            // False because not in db yet
            Assert.IsFalse(jobMgr.UpdateJob(exec));

            jobMgr.SaveJob(exec);
            Job exec2 = new Job("exec");
            Assert.IsTrue(jobMgr.UpdateJob(exec2));

            //Cleanup
            jobMgr.DeleteJob(exec2.Name);
        }

        [TestMethod]
        public void TestGetAllJobs()
        {
            Job exec = new Job("exec");
            Job ado = new Job("ado");
            jobMgr.SaveJob(exec);
            jobMgr.SaveJob(ado);

            // Returned list should contain 2 jobs
            IList<Job> jobs = jobMgr.GetAllJobs();
            Assert.IsTrue(jobs.Count == 2);

            // Cleanup
            jobMgr.DeleteJob(exec.Name);
            jobMgr.DeleteJob(ado.Name);
        }
    }
}
