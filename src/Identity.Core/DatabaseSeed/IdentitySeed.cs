using Identity.Core.Services.Constants;
using Identity.Infrastructure.DataAccess.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.DatabaseSeed
{
    //should be done on web api instead of core because of project dependencies
    public class IdentitySeed 
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var contexts = serviceProvider.GetServices<BaseContext>();
            var contantsFactory = serviceProvider.GetService<ConstantsFactory>();

            foreach (var context in contexts)
            {
                var contantService = contantsFactory.GetConstantsService(context.ContextName);

            }
        }
    }
}
