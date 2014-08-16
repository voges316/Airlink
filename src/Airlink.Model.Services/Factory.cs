using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Airlink.Model.Services
{
    public sealed class Factory
    {
        // Thread safe singleton
        private static readonly Factory instance = new Factory();

        static Factory() { }

        private Factory() { }

        public static Factory Instance
        {
            get 
            {
                Console.WriteLine("Making an instance of a factory");
                return instance;
            }
        }

        // Generic GetService Method
        public IService GetService(string name)
        {
            Console.WriteLine("Inside factory's getService method");
            Type type;
            Object obj = null;

            try
            {
                type = Type.GetType(GetImplName(name));
                Console.WriteLine("Trying to create an instance of the object: {0}", name);
                obj = Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured: {0}", e);
                throw e;
            }

            return (IService)obj;
        }

        // Returns fully qualified name of service for the string key, null if not found
        private string GetImplName(string serviceName)
        {
            Console.WriteLine("Inside getimplname method of factory");
            NameValueCollection settings = ConfigurationManager.AppSettings;
            Console.WriteLine("Succesfully read app properties");
            return settings.Get(serviceName);
        }
    }
}
