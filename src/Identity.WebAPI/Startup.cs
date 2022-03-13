using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Core;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;
using Identity.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebAPI
{
    public sealed class Startup
    {

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IContextAccessor, ContextAccessor>();
            
            services.AddServices();
        }

        public IServiceProvider ConfigureServices2(IServiceCollection services)
        {
            var connectionStrings = Configuration.GetSection("DevelopmentConnectionStrings");
            services.AddDbContext<Dev1Context>(options => options.UseSqlServer(connectionStrings.AsEnumerable().FirstOrDefault(_conn => _conn.Value == "dev_db_1").Value));
            // Autofac
            var builder = new ContainerBuilder();

            services.AddHttpContextAccessor();
            builder.RegisterType<Dev1Context>().As<BaseContext>();
            builder.RegisterType<Dev2Context>().As<BaseContext>();

            services.AddSingleton<IContextAccessor, ContextAccessor>();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddServices();

            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }
    }
}
