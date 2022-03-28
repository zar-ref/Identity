using Autofac;
using Identity.Infrastructure.DataAccess.DbContexts;
using Identity.Infrastructure.DataAccess.DbContexts.Dev1;
using Identity.Infrastructure.DataAccess.DbContexts.Dev2;

namespace Identity.WebAPI.Configurations
{
    public class DbContextsModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<Dev1Context>().As<BaseContext>();
            builder.RegisterType<Dev2Context>().As<BaseContext>();

           
        }
    }
}
