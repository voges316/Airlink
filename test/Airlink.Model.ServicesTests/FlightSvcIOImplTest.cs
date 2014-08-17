using Airlink.Model.Domain;
using Airlink.Model.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Airlink.Model.ServicesTests
{
    [TestClass]
    public class FlightSvcIOImplTest
    {
        // Service to use
        IFlightSvc fltServ = new FlightSvcIOImpl();

        [TestMethod]
        public void TestSaveDeleteFlight()
        {
            Flight aFlight = new Flight('a');
            Assert.IsTrue(fltServ.SaveFlight(aFlight));

            Assert.IsTrue(fltServ.DeleteFlight(aFlight));
        }

        [TestMethod]
        public void TestGetFlight()
        {
            Flight aFlight = new Flight('a');
            fltServ.SaveFlight(aFlight);

            Flight result = fltServ.GetFlight(aFlight.Name);
            Assert.AreEqual(aFlight, result);

            // Cleanup
            fltServ.DeleteFlight(aFlight);
        }

        [TestMethod]
        public void TestGetAllFlights()
        {
            Flight aFlight = new Flight('a');
            Flight bFlight = new Flight('b');
            fltServ.SaveFlight(aFlight);
            fltServ.SaveFlight(bFlight);

            // Flight list should contain 2 flights
            IList<Flight> flights = fltServ.GetAllFlights();
            Assert.IsTrue(flights.Count == 2);

            // Cleanup
            fltServ.DeleteFlight(aFlight);
            fltServ.DeleteFlight(bFlight);
        }
    }
}
