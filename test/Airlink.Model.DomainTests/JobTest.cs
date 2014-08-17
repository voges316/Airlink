using Airlink.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Airlink.Model.DomainTests
{
    [TestClass]
    public class JobTest
    {
        [TestMethod]
        public void TestEquals()
        {
            Job job1 = new Job("Squadron Exec");
            Job job2 = new Job("Squadron Exec");

            // Should be true since job names are equal
            Assert.AreEqual(job1, job2);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Job job1 = new Job("Squadron Exec");
            Job job2 = new Job("Director of Operations");

            // Should be unequal since names are not equal
            Assert.AreNotEqual(job1, job2);
        }

        [TestMethod]
        public void TestHashCode()
        {
            Job job1 = new Job("Squadron Exec");
            Job job2 = new Job("Squadron Exec");

            // Return same hash based on name
            Assert.AreEqual(job1.GetHashCode(), job2.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            Job job1 = new Job("Squadron Exec");
            string expected = "Job: Squadron Exec";
            string result = job1.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestValid()
        {
            Job job1 = new Job("Squadron Exec");
            // Should be valid since has name
            Assert.IsTrue(job1.Validate());
        }

        [TestMethod]
        public void TestNotValid()
        {
            Job job1 = new Job();
            // Shoudl be false since no info instantiated
            Assert.IsFalse(job1.Validate());
        }
    }
}
