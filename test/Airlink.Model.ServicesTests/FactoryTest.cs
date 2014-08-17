using Airlink.Model.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Airlink.Model.ServicesTests
{
    [TestClass]
    public class FactoryTest
    {
        // Single factory for each method to use
        Factory factory = Factory.Instance;
        
        // Each test uses the factory to get an interface, and then checks that 
        // it's the correct implementation
        [TestMethod]
        public void TestGetService()
        {
            IPersonnelSvc persSvc = (IPersonnelSvc)factory.GetService(typeof(IPersonnelSvc).Name);
            Assert.IsTrue(persSvc is PersonnelSvcIOImpl);

            IJobSvc jobSvc = (IJobSvc)factory.GetService(typeof(IJobSvc).Name);
            Assert.IsTrue(jobSvc is JobSvcIOImpl);

            IFlightSvc flightSvc = (IFlightSvc)factory.GetService(typeof(IFlightSvc).Name);
            Assert.IsTrue(flightSvc is FlightSvcIOImpl);
        }
    }
}
