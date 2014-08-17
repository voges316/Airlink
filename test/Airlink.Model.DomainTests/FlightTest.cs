using Airlink.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Airlink.Model.DomainTests
{
    [TestClass]
    public class FlightTest
    {
        [TestMethod]
        public void TestEquals()
        {
            Flight aFlight = new Flight('a');
            Flight aFlight2 = new Flight('a');

            // Should be true since flight names are equal
            Assert.AreEqual(aFlight, aFlight2);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Flight aFlight = new Flight('a');
            Flight bFlight = new Flight('b');

            // Should be unequal since names are not equal
            Assert.AreNotEqual(aFlight, bFlight);
        }

        [TestMethod]
        public void TestHashCode()
        {
            Flight aFlight = new Flight('a');
            Flight aFlight2 = new Flight('a');

            // Return same hash based on name
            Assert.AreEqual(aFlight.GetHashCode(), aFlight2.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            Flight aFlight = new Flight('a');
            string expected = "a Flight";
            string result = aFlight.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestValid()
        {
            Flight aFlight = new Flight('a');
            // Should be valid since has name
            Assert.IsTrue(aFlight.Validate());
        }

        [TestMethod]
        public void TestNotValid()
        {
            Flight flight1 = new Flight();
            // Shoudl be false since no info instantiated
            Assert.IsFalse(flight1.Validate());
        }
    }
}
