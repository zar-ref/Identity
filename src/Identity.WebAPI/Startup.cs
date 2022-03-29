using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Core;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;
using Identity.WebAPI.Configurations;
using Identity.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Identity.WebAPI
{
    public sealed class Startup
    {

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
       
            services.AddDbContexts(Configuration);
            services.AddServices();
            services.AddControllers();
            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity", Version = "v1" });
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        { 

            builder.RegisterModule(new DbContextsModule());
        }

        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity v1"));

            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            

        }

    }
}
