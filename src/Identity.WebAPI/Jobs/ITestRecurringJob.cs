namespace Identity.WebAPI.Jobs
{
    public interface ITestRecurringJob
    {
        Task Execute(string contextName);
    }
}