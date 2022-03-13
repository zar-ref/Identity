using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Infrastructure.DataAccess;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;
using Identity.WebAPI;
using Identity.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(_builder =>
{
  

    _builder.RegisterType<Dev1Context>().As<BaseContext>();
    _builder.RegisterType<Dev2Context>().As<BaseContext>();


});



// Add services to the container.
var connectionStrings = builder.Configuration.GetSection("DevelopmentConnectionStrings");
builder.Services.AddDbContext<Dev1Context>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("DevelopmentConnectionStrings:dev_db_1")));
builder.Services.AddDbContext<Dev2Context>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("DevelopmentConnectionStrings:dev_db_2")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IContextAccessor, ContextAccessor>();
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
builder.Services.AddServices();
//var startup = new Startup(builder.Configuration);
//startup.ConfigureServices2(builder.Services);
//startup.ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
