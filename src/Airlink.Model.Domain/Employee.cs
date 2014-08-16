using System;
using System.Collections.Generic;

namespace Airlink.Model.Domain
{
    [Serializable]
    public class Employee : Person
    {
        public Rank EmpRank { get; set; }
        public Flight Flight { get; set; }
        public List<Job> Jobs { get; set; }

        // Default constructor
        public Employee() : base()
        {
            Jobs = new List<Job>();
        }

        // Employees constructors require all info for Person as well as their rank.
        // If Address, family, & Jobs are not provided they are initialized but blank
        public Employee(string fName, string lName, string phoneNum, string email, Gender gender, Rank empRank) :
            base(fName, lName, phoneNum, email, gender)
        {
            EmpRank = empRank;
            Jobs = new List<Job>();
        }

        public Employee(string fName, string lName, string phoneNum, string email, Gender gender, Address address,
            Rank empRank) : base(fName, lName, phoneNum, email, gender, address)
        {
            EmpRank = empRank;
            Jobs = new List<Job>();
        }

        public Employee(string fName, string lName, string phoneNum, string email, Gender gender, 
            string street, string city, string state, string zip, Rank empRank) :
            base(fName, lName, phoneNum, email, gender, street, city, state, zip)
        {
            EmpRank = empRank;
            Jobs = new List<Job>();
        }

        // Adds rank to person's output
        public override string ToString()
        {
            return String.Format("{0}, Rank: {1}", base.ToString(), EmpRank.ToString());
        }
    }
}
