using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airlink.Model.Domain;
using Airlink.Model.Business;

namespace Airlink.Model.BusinessTests
{
    [TestClass]
    public class PersonnelManagerTest
    {
        PersonnelManager persMgr = PersonnelManager.Instance;

        [TestMethod]
        public void TestSaveMethod()
        {
            Person marge = new Person("marge", "simpson", "234567890", "marge@duff.com", Gender.FEMALE);

            Assert.IsTrue(persMgr.SavePerson(marge));

            // Cleanup
            persMgr.DeletePerson(marge.FirstName, marge.LastName);
        }

        [TestMethod]
        public void TestGetPerson()
        {
            // Setup
            Person marge = new Person("marge", "simpson", "234567890", "marge@duff.com", Gender.FEMALE);
            persMgr.SavePerson(marge);

            Person retrievedPerson = persMgr.GetPerson("marge", "simpson");
            Assert.AreEqual(marge, retrievedPerson);

            // Cleanup
            persMgr.DeletePerson(marge.FirstName, marge.LastName);
        }

        [TestMethod]
        public void TestUpdatePerson()
        {
            Person maggie = new Person("maggie", "simpson", "098765432", "mags@yahoo.com", Gender.FEMALE);

            // Returns false since person isn't in db yet
            Assert.IsFalse(persMgr.UpdatePerson(maggie));
            persMgr.SavePerson(maggie);
            Person maggieUpdate = new Person("maggie", "simpson", "123456789", "mags@gmail.com", Gender.FEMALE);

            // Returns true since changes are made
            Assert.IsTrue(persMgr.UpdatePerson(maggieUpdate));

            // Cleanup
            persMgr.DeletePerson(maggieUpdate.FirstName, maggieUpdate.LastName);
        }

        [TestMethod]
        public void TestDeletePerson()
        {
            Employee homer = new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1);

            // False because homer isn't there yet
            Assert.IsFalse(persMgr.DeletePerson(homer.FirstName, homer.LastName));

            persMgr.SavePerson(homer);
            Assert.IsTrue(persMgr.DeletePerson(homer.FirstName, homer.LastName));
        }
    }
}
