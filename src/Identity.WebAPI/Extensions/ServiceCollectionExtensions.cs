using Identity.Core;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;

namespace Identity.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddRepositoriees(this IServiceCollection services, IConfigurationSection connectionStrings)
        {
            return services
                .AddScoped<IDbConnection>(_serviceProvider =>
                {
                    var contextAccessor = _serviceProvider.GetRequiredService<IContextAccessor>();
                    var context = new ObjectContext(connectionStrings[$"dev_db_{contextAccessor.GetApplicationType()}"]);
                    return ((EntityConnection)context.Connection).StoreConnection;
                })
                 .AddScoped(_serviceProvider =>
                 {
                     var contextAccessor = _serviceProvider.GetRequiredService<IContextAccessor>();
                     var context = new ObjectContext(connectionStrings[$"dev_db_{contextAccessor.GetApplicationType()}"]);
                     context.ContextOptions.LazyLoadingEnabled = true;
                     return context;
                 })
                 .AddScoped<IUnityOfWork, UnityOfWork>();
        }
    }
}
