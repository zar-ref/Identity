using Hangfire;
using Hangfire.Client;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using Identity.Core.Services.Interfaces;
using Identity.Infrastructure.DataAccess;
using Identity.WebAPI.Jobs;

namespace Identity.WebAPI.Configurations
{
    public class RecurringHangfireConfiguration  
    {
        public static void InitializeJobs(IConfiguration config)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                try
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }
                catch
                {
                }
            }

            var queues = config.GetSection("Jobs").Get<string[]>();
            foreach (var contextName in queues)
            {
             
                RecurringJob.AddOrUpdate<ITestRecurringJob>("test_" + contextName, itj => itj.Execute(contextName), Cron.Minutely);
            }

        }

   
    }

    public class RecurringJobClientFilter : IClientFilter
    {
        public void OnCreated(CreatedContext filterContext)
        { 
        }

        public void OnCreating(CreatingContext filterContext)
        {
            if (filterContext.Parameters.TryGetValue("RecurringJobId", out object jobId) )
            {
                var x = jobId as string;
                var tenantId = jobId.ToString().Split("test_").Last();
                filterContext.SetJobParameter("ContextName", tenantId);
            }
        } 
    }



}
