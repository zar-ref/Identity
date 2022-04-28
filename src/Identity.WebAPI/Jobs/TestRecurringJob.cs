using Hangfire;
using Identity.Infrastructure.DataAccess;

namespace Identity.WebAPI.Jobs
{
    public class TestRecurringJob : ITestRecurringJob
    {
        private readonly IUnityOfWork _unityOfWork;
        public TestRecurringJob(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task Execute(string contextName)
        {
            await _unityOfWork.UserRepository.Login(contextName, "zarref@pit.com", "1234"); 
            await _unityOfWork.ManualDispose(contextName);
        }
    }
}
