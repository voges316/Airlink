using System;
using System.Collections.Generic;

namespace Airlink.Model.Domain
{
    [Serializable]
    public class Job
    {
        // Name of job in squadrons
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }

        public Job() 
        {
            Employees = new List<Employee>();
        }

        public Job(string name)
        {
            Name = name;
            Employees = new List<Employee>();
        }

        public override bool Equals(object obj)
        {
            // Check paramater not null
            if (obj == null)
            {
                return false;
            }

            // Check object is instance of type
            Job j = obj as Job;
            if ((System.Object)j == null)
            {
                return false;
            }

            // True if names match
            return Name == j.Name;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Name.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return String.Format("Job: {0}", Name);
        }

        public bool Validate()
        {
            if (Name == null || Name == "") { return false; }
            return true;
        }

    }
}
