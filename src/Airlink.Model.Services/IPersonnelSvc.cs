using Airlink.Model.Domain;
using System.Collections.Generic;

namespace Airlink.Model.Services
{
    // Contains methods for dealing with Employee and Person obj's,
    // as well as methods for assigning jobs and adding family members
    public interface IPersonnelSvc : IService
    {
        bool SavePerson(Person person);

        Person GetPerson(string fname, string lname);

        bool UpdatePerson(Person person);

        bool DeletePerson(string fname, string lname);

        IList<Person> GetPeople();

        Employee GetEmployee(string fname, string lname);

        bool AddJob(Employee emp, Job job);

        bool DeleteJob(Employee emp, Job job);

        IList<Employee> GetEmployees();

        bool AddFamily(Person primary, Person family);

        bool RemoveFamily(Person primary, Person family);
    }
}
