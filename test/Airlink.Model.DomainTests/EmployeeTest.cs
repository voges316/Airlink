using Airlink.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Airlink.Model.DomainTests
{
    [TestClass]
    public class EmployeeTest
    {
        // Presently Employee only overrides toString
        [TestMethod]
        public void TestToString()
        {
            Employee homer = new Employee("homer", "simpson", "123456789", "homer@duff.com", Gender.MALE, Rank.E1);
            string expected = "Name: homer simpson, Number: 123456789, Email: homer@duff.com, Rank: E1";
            string result = homer.ToString();
            Assert.AreEqual(expected, result);
        }
    }
}
