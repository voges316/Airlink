using Airlink.Model.Domain;
using Airlink.Model.Services;
using System;
using System.Collections.Generic;

namespace Airlink.Model.Business
{
    public class PersonnelManager : ManagerSupertype
    {
        // Thread safe singleton
        private static readonly PersonnelManager instance = new PersonnelManager();

        static PersonnelManager() { }

        private PersonnelManager() { }

        public static PersonnelManager Instance
        {
            get
            {
                Console.WriteLine("Making an instance of Personnel Manager");
                return instance;
            }
        }

        // Takes person object and returns false if not saved or exceptions are thrown
        public bool SavePerson(Person person)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.SavePerson(person);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error saving a person: {0}", e);
            }

            return result;
        }

        // Returns person obj that matches first/last name, and null otherwise or if an exception is thrown
        public Person GetPerson(string fname, string lname)
        {
            Person result = null;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.GetPerson(fname, lname);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error finding a person: {0}", e);
            }

            return result;
        }

        // Returns true if person is in database and is updated. False if not in db or exception thrown
        public bool UpdatePerson(Person person)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.UpdatePerson(person);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error updating a person: {0}", e);
            }

            return result;
        }

        // Deletes person in db based on first, last name. False if not found or error thrown
        public bool DeletePerson(string fname, string lname)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.DeletePerson(fname, lname);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error deleting a person: {0}", e);
            }

            return result;
        }

        // Returns IList of all people in db
        public IList<Person> GetPeople()
        {
            IList<Person> result;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.GetPeople();
                // return populated IList
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error getting a list of people: {0}", e);
            }

            // Returns blank list
            return result = new List<Person>();
        }

        // Returns IList of all employees in db
        // IList<Employee> GetEmployees();
        public IList<Employee> GetEmployees() 
        {
            IList<Employee> result;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.GetEmployees();
                // return populated IList
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error getting a list of employees: {0}", e);
            }

            // Returns a blank list
            return result = new List<Employee>();
        }        

        // Assign an existing job and employee to each other
        public bool AddJob(Employee emp, Job job)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try 
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.AddJob(emp, job);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error assigning a job to an employee: {0}", e);
            }

            return result;
        }

        // Remove an existing employee from a job
        public bool RemoveJob(Employee emp, Job job)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.DeleteJob(emp, job);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error removing a job from an employee: {0}", e);
            }

            return result;
        }

        // Assign two existing people as family members
        public bool AddFamily(Person primary, Person family)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.AddFamily(primary, family);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error assigning two people as family: {0}", e);
            }

            return result;
        }

        // Remove two existing people from family member list
        public bool RemoveFamily(Person primary, Person family)
        {
            bool result = false;
            IPersonnelSvc persSvc;
            try
            {
                persSvc = (IPersonnelSvc)GetService(typeof(IPersonnelSvc).Name);
                result = persSvc.RemoveFamily(primary, family);
            }
            catch (Exception e)
            {
                Console.WriteLine("PersonnelManager had an error removing two people from each others' family: {0}", e);
            }

            return result;
        }
    }
}
