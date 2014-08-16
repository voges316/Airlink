using Airlink.Model.Services;

namespace Airlink.Model.Business
{
    public abstract class ManagerSupertype
    {
        // Factory instance for all implementing managers to use
        Factory factory = Factory.Instance;

        // Generic GetService method for implementing managers to share
        protected IService GetService(string name)
        {
            return factory.GetService(name);
        }
    }
}
