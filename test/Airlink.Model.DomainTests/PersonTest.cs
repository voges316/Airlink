using Airlink.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Airlink.Model.DomainTests
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void TestEquals()
        {
            Person homer = new Person("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE);
            Person homer2 = new Person("homer", "simpson", "234567890", "homes@duff.com", Gender.MALE);

            // Should be true since first and last name are equal
            Assert.AreEqual(homer, homer2);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Person homer = new Person("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE);
            Person marge = new Person("marge", "simpson", "234567890", "marge@duff.com", Gender.FEMALE);

            // Should be unequal since first and last name are not equal
            Assert.AreNotEqual(homer, marge);
        }

        [TestMethod]
        public void TestHashCode()
        {
            Person homer = new Person("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE);
            Person homer2 = new Person("homer", "simpson", "234567890", "homes@duff.com", Gender.MALE);

            // Return same hash based on first and last names
            Assert.AreEqual(homer.GetHashCode(), homer2.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            Person homer = new Person("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE);
            string expected = "Name: homer simpson, Number: 123456789, Email: homer@duff.com";
            string result = homer.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestValid()
        {
            Person homer = new Person("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE);
            // Should be valid since contains full name and email
            Assert.IsTrue(homer.Validate());
        }

        [TestMethod]
        public void TestNotValid()
        {
            Person homer = new Person();
            // Shoudl be false since no info instantiated
            Assert.IsFalse(homer.Validate());
        }
    }
}
