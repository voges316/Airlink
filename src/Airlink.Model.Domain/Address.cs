using System;

namespace Airlink.Model.Domain
{
    [Serializable]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address() { }

        public Address(string street, string city, string state, string zip)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zip;
        }

        public override bool Equals(object obj)
        {
            // Check paramater not null
            if (obj == null)
            {
                return false;
            }

            // Check object is instance of type
            Address a = obj as Address;
            if ((System.Object)a == null)
            {
                return false;
            }

            // True if properties match
            return Street.Equals(a.Street) && City.Equals(a.City) && State.Equals(a.State) && ZipCode.Equals(a.ZipCode);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            // Uses same parameters as testing for equality
            hash = hash * 23 + Street.GetHashCode();
            hash = hash * 23 + City.GetHashCode();
            hash = hash * 23 + State.GetHashCode();
            hash = hash * 23 + ZipCode.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return String.Format("Address: {0}\n{1}, {2} {3}", Street, City, State, ZipCode);
        }

        // Address requires each instance to contain info
        public bool Validate()
        {
            if (Street == null || Street == "") { return false; }
            if (City == null || City == "") { return false; }
            if (State == null || State == "") { return false; }
            if (ZipCode == null || ZipCode == "") { return false; }
            return true;
        }

    }
}
