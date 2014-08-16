using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airlink.Model.Services
{
    public class PersonnelSvcIOImpl : IPersonnelSvc
    {
        // Save person and returns true
        // Returns false if a person with the same first/last name already exists
        public bool SavePerson(Person person)
        {
            bool result = false;
            string name = person.FirstName + person.LastName;
            try
            {                
                DbCollections.People.Add(name, person);
                result = true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("An element with key {0} already exists", name);
            }
            return result;
        }

        // Retrieves person based on first, last name. Returns null if not found
        public Person GetPerson(string fname, string lname)
        {
            Person person = null;
            string name = fname + lname;
            DbCollections.People.TryGetValue(name, out person);            

            return person;
        }

        // If person name exists overwrites it with new person info and returns true. false if not found
        public bool UpdatePerson(Person person)
        {
            bool result = false;
            string name = person.FirstName + person.LastName;
            if (DbCollections.People.ContainsKey(name))
            {
                DbCollections.People[name] = person;
                result = true;
            }

            return result;
        }

        // true if element found and removed. false if key is not found
        public bool DeletePerson(string fname, string lname)
        {
            // Check for family and then check for jobs
            Person primaryPerson = GetPerson(fname, lname);
            if (primaryPerson != null)
            {
                if (primaryPerson.Family != null && primaryPerson.Family.Count > 0)
                {
                    foreach (Person family in primaryPerson.Family)
                    {
                        // This is probably overkill, but I wan't to keep the 'approach' that other methods use
                        // Retrieve family from db, make changes, then persist the changes.
                        // Probably not needed since the changes are made on the actual obj in memory with my
                        // current approach using serialization, but it's consistent.
                        Person f = GetPerson(family.FirstName, family.LastName);
                        f.Family.Remove(primaryPerson);
                        UpdatePerson(f);
                    }
                }

                if (primaryPerson is Employee)
                {
                    Employee emp = (Employee)primaryPerson;
                    if (emp.Jobs != null && emp.Jobs.Count > 0) 
                    {
                        IJobSvc jService = new JobSvcIOImpl();
                        foreach (Job empJob in emp.Jobs)
                        {
                            // Same methodology. Overkill, but consistent approach
                            Job j = jService.GetJob(empJob.Name);
                            j.Employees.Remove(emp);
                            jService.UpdateJob(j);
                        }
                    }
                }
            }
            
            string name = fname + lname;
            return DbCollections.People.Remove(name);
        }

        public IList<Person> GetPeople()
        {
            return DbCollections.People.Values.ToList();
        }

        // Retrieves employee based on first, last name. Returns null if not found or name is
        // a person, not an employee
        public Employee GetEmployee(string fname, string lname)
        {
            Employee emp = null;
            Person person = GetPerson(fname, lname);
            if (person != null && person is Employee)
            {
                emp = (Employee)person;
            }
            return emp;
        }

        // Looks up both employee and job to make sure they already exist
        // Returns true after adding each one to the other's list and saving the changes
        public bool AddJob(Employee emp, Job job)
        {
            bool result = false;
            if (DbCollections.People.ContainsKey(emp.FirstName + emp.LastName) &&
                DbCollections.SquadronJobs.ContainsKey(job.Name))
            {
                // Make changes to each object
                emp.Jobs.Add(job);
                job.Employees.Add(emp);

                // Persist the changes
                UpdatePerson(emp);
                new JobSvcIOImpl().UpdateJob(job);

                result = true;
            }

            return result;
        }

        // Looks up both employee and job to make sure they already exist
        // Returns true after removing each one from the other's list and saving the changes
        public bool DeleteJob(Employee emp, Job job)
        {
            bool result = false;
            if (DbCollections.People.ContainsKey(emp.FirstName + emp.LastName) &&
                DbCollections.SquadronJobs.ContainsKey(job.Name))
            {
                // Make changes to each object
                emp.Jobs.Remove(job);
                job.Employees.Remove(emp);

                // Persist the changes
                UpdatePerson(emp);
                new JobSvcIOImpl().UpdateJob(job);

                result = true;
            }
            
            return result;
        }

        public IList<Employee> GetEmployees()
        {
            // The main collection stores people as person objects, so have to extract the 
            // employee objects first and then return them in a list
            IEnumerable<Person> emps = DbCollections.People.Values.Where(person => (person is Employee));
            IList<Employee> employees = new List<Employee>();

            // Explicitly cast each obj to employee one by one for now
            foreach(Person p in emps) {
                employees.Add((Employee)p);
            }

            return employees;
        }

        // Looks up both people to make sure they already exist
        // If so, then adds each to the other's family list
        public bool AddFamily(Person primary, Person family)
        {
            bool result = false;
            if (DbCollections.People.ContainsKey(primary.FirstName + primary.LastName) &&
                DbCollections.People.ContainsKey(family.FirstName + family.LastName)) 
            {
                // Make changes
                primary.Family.Add(family);
                family.Family.Add(primary);

                // Persist the changes
                UpdatePerson(primary);
                UpdatePerson(family);

                result = true;
            }

            return result;
        }

        // Looks up both people to make sure they already exist in db
        // If so, then removes each other from their family lists
        public bool RemoveFamily(Person primary, Person family)
        {
            bool result = false;
            if (DbCollections.People.ContainsKey(primary.FirstName + primary.LastName) &&
                DbCollections.People.ContainsKey(family.FirstName + family.LastName))
            {
                // Make changes
                primary.Family.Remove(family);
                family.Family.Remove(primary);

                // Persist the changes
                UpdatePerson(primary);
                UpdatePerson(family);

                result = true;
            }

            return result;
        }
    }
}
