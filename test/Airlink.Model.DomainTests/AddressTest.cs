using Airlink.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Airlink.Model.DomainTests
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void TestEquals()
        {
            Address add1 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");
            Address add2 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");

            // Should be true since properties match
            Assert.AreEqual(add1, add2);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Address add1 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");
            Address add2 = new Address("744 Evergreen Terrace", "Springfield", "WA", "12345");

            // Should be unequal since address is next door
            Assert.AreNotEqual(add1, add2);
        }

        [TestMethod]
        public void TestHashCode()
        {
            Address add1 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");
            Address add2 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");

            // Return same hash based on properties
            Assert.AreEqual(add1.GetHashCode(), add2.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            Address add1 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");
            string expected = "Address: 742 Evergreen Terrace\nSpringfield, WA 12345";
            string result = add1.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestValid()
        {
            Address add1 = new Address("742 Evergreen Terrace", "Springfield", "WA", "12345");
            // Should be valid since contains full properties
            Assert.IsTrue(add1.Validate());
        }

        [TestMethod]
        public void TestNotValid()
        {
            Address add1 = new Address();
            // Should be false since no info instantiated
            Assert.IsFalse(add1.Validate());
        }
    }
}
