using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Core;
using Identity.Core.DatabaseSeed;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;
using Identity.Infrastructure.WebSocket.Client; 
using Identity.WebAPI.Configurations;
using Identity.WebAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Identity.WebAPI
{
    public sealed class Startup
    {

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContexts(Configuration);
            services
                .AddIdentity<Account, IdentityRole<int>>()
                .AddEntityFrameworkStores<Dev1Context>()
                .AddEntityFrameworkStores<Dev2Context>()
                .AddDefaultTokenProviders();

            WsManager.Instance.Init(Configuration);
            services.AddAuthentication(_options =>
            {
                _options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                _options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                _options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                }); ;
            services.AddContantsServices();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity v1"));

            }



            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            IdentitySeed.Initialize(app.ApplicationServices);
            //WsManager.Instance.Init(Configuration.GetSection("JWT"));
        }

    }
}
