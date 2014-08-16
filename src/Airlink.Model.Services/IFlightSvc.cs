using Airlink.Model.Domain;
using System.Collections.Generic;

namespace Airlink.Model.Services
{
    public interface IFlightSvc : IService
    {
        bool SaveFlight(Flight flight);

        Flight GetFlight(char flightName);

        bool UpdateFlight(Flight flight);

        bool DeleteFlight(Flight flight);

        IList<Flight> GetAllFlights();
    }
}
