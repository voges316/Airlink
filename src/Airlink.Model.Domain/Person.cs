using System;
using System.Collections.Generic;

namespace Airlink.Model.Domain
{
    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public List<Person> Family { get; set; }

        // Default Constructor
        public Person() 
        {
            Address = new Address();
            Family = new List<Person>();
        }

        // For constructors, a new person only requires name, phone, email, and gender.
        // If Address & family are not provided they are initialized but blank
        public Person(string fName, string lName, string phoneNum, string email, Gender gender)
        {
            FirstName = fName;
            LastName = lName;
            PhoneNumber = phoneNum;
            Email = email;
            Gender = gender;
            Address = new Address();
            Family = new List<Person>();
        }

        public Person(string fName, string lName, string phoneNum, string email, Gender gender,
            Address address)
        {
            FirstName = fName;
            LastName = lName;
            PhoneNumber = phoneNum;
            Email = email;
            Gender = gender;
            Address = address;
            Family = new List<Person>();
        }

        public Person(string fName, string lName, string phoneNum, string email, Gender gender,
            string street, string city, string state, string zip)
        {
            FirstName = fName;
            LastName = lName;
            PhoneNumber = phoneNum;
            Email = email;
            Gender = gender;
            Address = new Address(street, city, state, zip);
            Family = new List<Person>();
        }

        // Person is equal if first & last name match
        public override bool Equals(object obj)
        {
            // Check paramater not null
            if (obj == null)
            {
                return false;
            }

            // Check object is instance of type
            Person p = obj as Person;
            if ((System.Object)p == null)
            {
                return false;
            }

            // True if names match
            return (FirstName.Equals(p.FirstName)) && (LastName.Equals(p.LastName));
        }

        // Uses a person's first and last name, just like the equals method
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + FirstName.GetHashCode();
            hash = hash * 23 + LastName.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return String.Format("Name: {0} {1}, Number: {2}, Email: {3}", FirstName, LastName, PhoneNumber, Email);
        }

        // Person requires the first & last name and an email to be valid
        public bool Validate()
        {
            if (FirstName == null || FirstName == "") { return false; }
            if (LastName == null || LastName == "") { return false; }
            if (Email == null || Email == "") { return false; }
            return true;
        }

    }
}
