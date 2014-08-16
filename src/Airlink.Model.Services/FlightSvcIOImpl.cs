using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airlink.Model.Services
{
    public class FlightSvcIOImpl : IFlightSvc
    {
        public bool SaveFlight(Flight flight)
        {
            bool result = false;
            char name = flight.Name;
            try
            {
                DbCollections.SquadronFlights.Add(name, flight);
                result = true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("A flight element with key {0} already exists", name);
            }

            return result;
        }

        // Returns the flight if it exists, otherwise null
        public Flight GetFlight(char flightName)
        {
            Flight flight = null;
            DbCollections.SquadronFlights.TryGetValue(flightName, out flight);

            return flight;
        }

        // If flight name (char) exists overwrites it with new flight info and returns true. false if not found
        public bool UpdateFlight(Flight flight)
        {
            bool result = false;
            if (DbCollections.SquadronFlights.ContainsKey(flight.Name))
            {
                DbCollections.SquadronFlights[flight.Name] = flight;
                result = true;
            }

            return result;
        }

        // true if element found and removed. false if key is not found
        public bool DeleteFlight(Flight flight)
        {
            // Check for employee dependencies using similar approach used in DeletePerson
            /*
             * Job job = GetJob(jobName);
            if (job != null && job.Employees.Count > 0)
            {
                IPersonnelSvc pServ = new PersonnelSvcIOImpl();
                foreach (Employee emp in job.Employees)
                {
                    Employee e = pServ.GetEmployee(emp.FirstName, emp.LastName);
                    e.Jobs.Remove(job);
                    pServ.UpdatePerson(e);
                }
            }
             */
            Flight flt = GetFlight(flight.Name);
            if (flt != null && flt.Employees.Count > 0)
            {
                IPersonnelSvc pServ = new PersonnelSvcIOImpl();
                foreach (Employee emp in flt.Employees)
                {
                    Employee e = pServ.GetEmployee(emp.FirstName, emp.LastName);
                    // TODO, remove list of flights
                    // e.Flights.Remove(flight);

                    // if not null and equal, reset to new (blank) flight
                    if (e.Flight != null && e.Flight == flt)
                    {
                        e.Flight = new Flight();
                    }

                    pServ.UpdatePerson(e);
                }
            }

            return DbCollections.SquadronFlights.Remove(flight.Name);
        }

        // Returns IList of all flights. IList is empty if none exist
        public IList<Flight> GetAllFlights()
        {
            /*List<Flight> flights = new List<Flight>();
            // Collection is just an array of char's, so have to create the flight obj
            // for each char value
            foreach (char c in DbCollections.SquadronFlights)
            {
                flights.Add(new Flight(c));
            }

            return flights;*/
            return DbCollections.SquadronFlights.Values.ToList();
        }
    }
}
