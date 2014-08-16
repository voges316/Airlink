using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Airlink.Model.Services
{
    // This class contains the static data collections for shared use.
    // A static constructor and common finalizer is used to serialize/deserialize the data
    // on class load and shutdown
    public sealed class DbCollections
    {
        // Contains master collections for domain obj's
        public static IDictionary<string, Person> People { get; set; }    // key is firstname + lastname
        public static IDictionary<string, Job> SquadronJobs { get; set; } // key is jobName
        public static IDictionary<char, Flight> SquadronFlights { get; set; }            // Just a set of char values

        private static string FileName = ConfigurationManager.AppSettings.Get("database");

        // Singleton with static init
        private static readonly DbCollections instance = new DbCollections();

        private DbCollections() 
        {
            InitializeCollections();
        }

        public static DbCollections GetInstance 
        {
            get 
            {
                return instance;
            }
        }


        // Finalizer that serializes data
        ~DbCollections()
        {
            PersistCollections();
        }

        // Method reads in the 'container' object from the file and extracts current list data
        private static void InitializeCollections()
        {
            CollectionContainer c;
            BinaryFormatter binFormat = new BinaryFormatter();
            Stream fStream = File.Open(FileName, FileMode.OpenOrCreate);

            try
            {                
                c = (CollectionContainer)binFormat.Deserialize(fStream);
            }
            catch (Exception ex)
            {
                // If file fails to load or did not exist, create a new, empty container object
                Console.WriteLine("File did not exist. Will create new one: {0}", ex.Message);
                c = new CollectionContainer();
            }
            finally
            {
                if (fStream != null) fStream.Close();
            }

            // Extract collection data for use. If data file didn't exist collections 
            // should be empty
            People = c.peopleHidden;
            SquadronJobs = c.jobsHidden;
            SquadronFlights = c.flightsHidden;

            //Console.WriteLine("exiting init collections method");
        }

        // Creates new 'container' object, stores the current data and serializes it to a file
        private static void PersistCollections()
        {
            // Prepare data 'container' for storage
 	        CollectionContainer c = new CollectionContainer();
            c.peopleHidden = People;
            c.jobsHidden = SquadronJobs;
            c.flightsHidden = SquadronFlights;


            // Serialize the data 'container'
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(FileName,
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, c);
            }

        }

        // This is a pure convenience object that holds the collections and simplifies 
        // serialing/deserializing the data
        [Serializable]
        private class CollectionContainer
        {
            public IDictionary<string, Person> peopleHidden { get; set;}
            public IDictionary<string, Job> jobsHidden { get; set;}
            public IDictionary<char, Flight> flightsHidden {get; set;}
            
            public CollectionContainer() {
                peopleHidden = new Dictionary<string, Person>();
                jobsHidden = new Dictionary<string, Job>();
                flightsHidden = new Dictionary<char, Flight>();
            }
        }
    }
}
