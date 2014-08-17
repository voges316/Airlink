using Airlink.Model.Business;
using Airlink.Model.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Airlink.Model.BusinessTests
{
    [TestClass]
    public class FlightManagerTest
    {
        // Service to use
        FlightManager fltMgr = FlightManager.Instance;

        [TestMethod]
        public void TestSaveDeleteFlight()
        {
            Flight aFlight = new Flight('a');
            Assert.IsTrue(fltMgr.SaveFlight(aFlight));

            Assert.IsTrue(fltMgr.DeleteFlight(aFlight));
        }

        [TestMethod]
        public void TestGetFlight()
        {
            Flight aFlight = new Flight('a');
            fltMgr.SaveFlight(aFlight);

            Flight result = fltMgr.GetFlight(aFlight.Name);
            Assert.AreEqual(aFlight, result);

            // Cleanup
            fltMgr.DeleteFlight(aFlight);
        }

        [TestMethod]
        public void TestGetAllFlights()
        {
            Flight aFlight = new Flight('a');
            Flight bFlight = new Flight('b');
            fltMgr.SaveFlight(aFlight);
            fltMgr.SaveFlight(bFlight);

            // Flight list should contain 2 flights
            IList<Flight> flights = fltMgr.GetAllFlights();
            Assert.IsTrue(flights.Count == 2);

            // Cleanup
            fltMgr.DeleteFlight(aFlight);
            fltMgr.DeleteFlight(bFlight);
        }
    }
}
