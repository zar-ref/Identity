using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Core;
using Identity.Core.Services;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;

namespace Identity.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfigurationSection connectionStrings)
        {
            return services
                .AddScoped<IDbConnection>(_serviceProvider =>
                {
                    var contextAccessor = _serviceProvider.GetRequiredService<IContextAccessor>();
                    var context = new ObjectContext(connectionStrings[$"dev_db_{contextAccessor.GetApplicationId()}"]);
                    return ((EntityConnection)context.Connection).StoreConnection;
                })
                 .AddScoped(_serviceProvider =>
                 {
                     var contextAccessor = _serviceProvider.GetRequiredService<IContextAccessor>();
                     var context = new ObjectContext(connectionStrings[$"dev_db_{contextAccessor.GetApplicationId()}"]);
                     context.ContextOptions.LazyLoadingEnabled = true;
                     return context;
                 })
                 .AddScoped<IUnityOfWork, UnityOfWork>();
        }

        //public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfigurationSection connectionStrings)
        //{
        //    services.AddDbContext<Dev1Context>(options => options.UseSqlServer(connectionStrings.AsEnumerable().FirstOrDefault(_conn => _conn.Value == "dev_db_1").Value));
            
        //    var builder = new ContainerBuilder();

        //    builder.RegisterType<Dev1Context>().As<BaseContext>();

        //    builder.Populate(services);
        //    return new AutofacServiceProvider(builder.Build());
        //}
    }
}
