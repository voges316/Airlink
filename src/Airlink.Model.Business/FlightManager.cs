using Airlink.Model.Domain;
using Airlink.Model.Services;
using System;
using System.Collections.Generic;

namespace Airlink.Model.Business
{
    public class FlightManager : ManagerSupertype
    {
        // Thread safe singleton
        private static readonly FlightManager instance = new FlightManager();

        static FlightManager() { }

        private FlightManager() { }

        public static FlightManager Instance
        {
            get
            {
                Console.WriteLine("Making an instance of Flight Manager");
                return instance;
            }
        }


        // Takes flight object and returns false if not saved or exceptions are thrown
        public bool SaveFlight(Flight flight)
        {
            bool result = false;
            IFlightSvc fltSvc;
            try
            {
                fltSvc = (IFlightSvc)GetService(typeof(IFlightSvc).Name);
                result = fltSvc.SaveFlight(flight);
            }
            catch (Exception e)
            {
                Console.WriteLine("FlightManager had an error saving a flight: {0}", e);
            }

            return result;
        }

        // Returns flight obj that matches char name, and null otherwise or if an exception is thrown
        public Flight GetFlight(char flightName)
        {
            Flight result = null;
            IFlightSvc fltSvc;
            try
            {
                fltSvc = (IFlightSvc)GetService(typeof(IFlightSvc).Name);
                result = fltSvc.GetFlight(flightName);
            }
            catch (Exception e)
            {
                Console.WriteLine("FlightManager had an error finding a flight: {0}", e);
            }

            return result;
        }

        // // Returns true if flight is in database and is updated. False if not in db or exception thrown
        public bool UpdateFlight(Flight flight)
        {
            bool result = false;
            IFlightSvc fltSvc;
            try
            {
                fltSvc = (IFlightSvc)GetService(typeof(IFlightSvc).Name);
                result = fltSvc.UpdateFlight(flight);
            }
            catch (Exception e)
            {
                Console.WriteLine("FlightManager had an error updating a flight: {0}", e);
            }

            return result;
        }

        // Deletes flight in db. False if not found or error thrown
        public bool DeleteFlight(Flight flight)
        {
            bool result = false;
            IFlightSvc fltSvc;
            try
            {
                fltSvc = (IFlightSvc)GetService(typeof(IFlightSvc).Name);
                result = fltSvc.DeleteFlight(flight);
            }
            catch (Exception e)
            {
                Console.WriteLine("FlightManager had an error deleting a flight: {0}", e);
            }

            return result;
        }

        // Returns IList of all flights in db
        public IList<Flight> GetAllFlights()
        {
            IList<Flight> result;
            IFlightSvc fltSvc;
            try
            {
                fltSvc = (IFlightSvc)GetService(typeof(IFlightSvc).Name);
                result = fltSvc.GetAllFlights();
                // return populated IList
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("FlightManager had an error getting a list of flights: {0}", e);
            }

            // Returns blank list
            return result = new List<Flight>();
        }
    }
}
