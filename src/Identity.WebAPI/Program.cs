using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;
using Identity.WebAPI;
using Identity.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore; 

namespace Identity.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
       
            CreateHostBuilder(args).Build().Run();
          
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}