using System;
using System.Collections.Generic;

namespace Airlink.Model.Domain
{
    [Serializable]
    public class Flight
    {
        // Name is a single char representing a squadron flight, typically for approx 20 personnel
        // Ex: a for A Flight, B for B Flight, etc.
        public char Name { get; set; }
        public List<Employee> Employees { get; set; }

        public Flight() 
        {
            Employees = new List<Employee>();
        }

        public Flight(char name)
        {
            Name = name;
            Employees = new List<Employee>();
        }

        // Equal based only on flight name
        public override bool Equals(object obj)
        {
            // Check paramater not null
            if (obj == null)
            {
                return false;
            }

            // Check object is instance of type
            Flight f = obj as Flight;
            if ((System.Object)f == null)
            {
                return false;
            }

            // True if flights match
            return Name == f.Name;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Name.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return String.Format("{0} Flight", Name);
        }

        // Checks Name is not the default char value
        public bool Validate()
        {
            if (Name == '\0') { return false; }
            return true;
        }
    }
}
