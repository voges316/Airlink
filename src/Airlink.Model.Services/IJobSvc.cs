using Airlink.Model.Domain;
using System.Collections.Generic;

namespace Airlink.Model.Services
{
    public interface IJobSvc : IService
    {
        bool SaveJob(Job job);

        Job GetJob(string jobName);

        bool UpdateJob(Job job);

        bool DeleteJob(string jobName);

        IList<Job> GetAllJobs();
    }
}
