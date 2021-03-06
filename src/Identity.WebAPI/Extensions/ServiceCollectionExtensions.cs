using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Core;
using Identity.Core.Services;
using Identity.Core.Services.Constants;
using Identity.Core.Services.Interfaces;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;
using Identity.WebAPI.Jobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<ITestRecurringJob, TestRecurringJob>();
        }
        public static IServiceCollection AddContantsServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ConstantsFactory>()
                    .AddScoped<BaseConstantsService>()
                    .AddScoped<IBaseContantsService, BaseConstantsService>(s => s.GetService<BaseConstantsService>())
                    .AddScoped<Dev2ContantsService>()
                    .AddScoped<IBaseContantsService, Dev2ContantsService>(s => s.GetService<Dev2ContantsService>());


        }
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration config)
        {
           
            return services
                .AddDbContext<Dev1Context>(options => options.UseSqlServer(config.GetValue<string>("DevelopmentConnectionStrings:dev_db_1")))
                .AddDbContext<Dev2Context>(options => options.UseSqlServer(config.GetValue<string>("DevelopmentConnectionStrings:dev_db_2")))
                .AddHttpContextAccessor()
                .AddSingleton<IContextAccessor, ContextAccessor>()
                .AddScoped<IUnityOfWork, UnityOfWork>();
        }
      

    }
}
