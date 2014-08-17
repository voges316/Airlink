using Airlink.Model.Domain;
using Airlink.Model.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Airlink.Model.ServicesTests
{
    [TestClass]
    public class PersonnelSvcIOImplTest
    {
        // Services to use
        IPersonnelSvc persImpl = new PersonnelSvcIOImpl();
        IJobSvc jobImpl = new JobSvcIOImpl();   

        [TestMethod]
        public void TestSavePerson()
        {
            Person marge = new Person("marge", "simpson", "234567890", "marge@duff.com", Gender.FEMALE);
            
            Assert.IsTrue(persImpl.SavePerson(marge));
            // Should now return false because marge already exists
            Assert.IsFalse(persImpl.SavePerson(marge));

            // Cleanup
            persImpl.DeletePerson(marge.FirstName, marge.LastName);
        }

        [TestMethod]
        public void TestGetPerson()
        {
            Employee bart = new Employee("bart", "simpson", "987654321", "bart@gmail.com", Gender.MALE, Rank.E2);
            
            persImpl.SavePerson(bart);
            Person retrievedPerson = persImpl.GetPerson("bart", "simpson");
            Assert.AreEqual(bart, retrievedPerson);

            // Cleanup
            persImpl.DeletePerson(bart.FirstName, bart.LastName);
        }

        [TestMethod]
        public void TestUpdatePerson()
        {
            Person maggie = new Person("maggie", "simpson", "098765432", "mags@yahoo.com", Gender.FEMALE);

            // Returns false since person isn't in db yet
            Assert.IsFalse(persImpl.UpdatePerson(maggie));
            persImpl.SavePerson(maggie);
            Person maggieUpdate = new Person("maggie", "simpson", "123456789", "mags@gmail.com", Gender.FEMALE);

            // Returns true since changes are made
            Assert.IsTrue(persImpl.UpdatePerson(maggieUpdate));

            // Cleanup
            persImpl.DeletePerson(maggieUpdate.FirstName, maggieUpdate.LastName);
        }

        [TestMethod]
        public void TestDeletePerson()
        {
            Employee homer = new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1);

            // False because homer isn't there yet
            Assert.IsFalse(persImpl.DeletePerson(homer.FirstName, homer.LastName));

            persImpl.SavePerson(homer);
            Assert.IsTrue(persImpl.DeletePerson(homer.FirstName, homer.LastName));
        }

        [TestMethod]
        public void TestGetEmployee()
        {
            Employee homer = new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1);

            // False because homer not in db
            Assert.IsNull(persImpl.GetEmployee(homer.FirstName, homer.LastName));

            persImpl.SavePerson(homer);
            Employee homer2 = persImpl.GetEmployee(homer.FirstName, homer.LastName);
            Assert.AreEqual(homer, homer2);

            // Cleanup
            persImpl.DeletePerson(homer.FirstName, homer.LastName);
        }

        [TestMethod]
        public void TestAddDeleteJob()
        {
            Employee homer = new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1);
            Job exec = new Job("exec");

            // Neither object exists yet in db
            Assert.IsFalse(persImpl.AddJob(homer, exec));

            // Add info and verify persistence
            persImpl.SavePerson(homer);
            jobImpl.SaveJob(exec);
            Assert.IsTrue(persImpl.AddJob(homer, exec));

            // Now retrieve the objects and verify they contain the changes
            Employee emp = persImpl.GetEmployee(homer.FirstName, homer.LastName);
            Job job = jobImpl.GetJob(exec.Name);
            Assert.AreEqual(exec, emp.Jobs[0]);
            Assert.AreEqual(homer, job.Employees[0]);

            // Test Deleting by overwriting initial objects and verifying changes
            Assert.IsTrue(persImpl.DeleteJob(emp, job));
            homer = persImpl.GetEmployee(homer.FirstName, homer.LastName);
            exec = jobImpl.GetJob(exec.Name);
            Assert.IsTrue(homer.Jobs.Count == 0 && exec.Employees.Count == 0);

            // Cleanup & Delete
            Assert.IsTrue(persImpl.DeletePerson(homer.FirstName, homer.LastName));
            Assert.IsTrue(jobImpl.DeleteJob(exec.Name));
        }

        [TestMethod]
        public void TestGetEmployees()
        {
            // Prep the list
            Person[] input = new [] {
                new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1),
                new Person("marge", "simpson", "234567890", "marge@duff.com", Gender.FEMALE),
                new Employee("bart", "simpson", "987654321", "bart@gmail.com", Gender.MALE, Rank.E2),
                new Person("maggie", "simpson", "098765432", "mags@yahoo.com", Gender.FEMALE)
            };
            foreach (Person p in input)
            {
                persImpl.SavePerson(p);
            }

            IList<Employee> employees = persImpl.GetEmployees();
            // Should return two employees
            Assert.IsTrue(employees.Count == 2);

            // Cleanup
            foreach (Person p in input)
            {
                persImpl.DeletePerson(p.FirstName, p.LastName);
            }
        }

        [TestMethod]
        public void TestAddFamily()
        {
            Employee homer = new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1);
            Person marge = new Person("marge", "simpson", "234567890", "marge@duff.com", Gender.FEMALE);

            // Neither exists in db yet
            Assert.IsFalse(persImpl.AddFamily(homer, marge));

            // Add info and verify persistence
            persImpl.SavePerson(homer);
            persImpl.SavePerson(marge);
            Assert.IsTrue(persImpl.AddFamily(homer, marge));

            // Now retrieve objects and verify they contain the changes
            Employee emp = persImpl.GetEmployee(homer.FirstName, homer.LastName);
            Person pers = persImpl.GetPerson(marge.FirstName, marge.LastName);
            Assert.AreEqual(homer, pers.Family[0]);
            Assert.AreEqual(marge, emp.Family[0]);

            // Cleanup
            persImpl.DeletePerson(homer.FirstName, homer.LastName);
            persImpl.DeletePerson(marge.FirstName, marge.LastName);
        }
    }
}
