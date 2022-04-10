using Identity.Core.Services.Constants;
using Identity.Entities.Entities.Identity;
using Identity.Infrastructure.DataAccess.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var contexts = serviceProvider.GetServices<BaseContext>();
            ConstructDbContextIdentityManagers(contexts.ToList());
            var constantsFactory = serviceProvider.GetService<ConstantsFactory>();

            foreach (var context in contexts)
            {
                var constantService = constantsFactory.GetConstantsService(context.ContextName);         

                if (!context.Users.Any(u => u.Email == "zarref@pit.com"))
                {
                    Entities.Entities.Identity.Account account = new Entities.Entities.Identity.Account();
                    account.IdentityRoles = new List<IdentityRole<int>>() { new IdentityRole<int>(GenericConstants.ApplicationRoles.Admin.ToString()) };
                    account.Email = "zarref@pit.com";
                    account.ApplicationCode = context.ContextName;
                    account.PasswordHash = context.UserManager.PasswordHasher.HashPassword(account, "1234");
                    await context.UserManager.CreateAsync(account);
                    await context.SaveChangesAsync();
                }

            }


        }

        private static void ConstructDbContextIdentityManagers(ICollection<BaseContext> dbContexts)
        {
            foreach (var context in dbContexts)
            {
                var um = new UserStore<Account, IdentityRole<int>, BaseContext, int>(context);
                var um2 = new Microsoft.AspNetCore.Identity.UserManager<Account>(um, null, new PasswordHasher<Account>(), null, null, null, null, null, null);

                var rm = new RoleStore<IdentityRole<int>, BaseContext, int>(context);
                context.UserManager = um2;
                context.RoleManager = new Microsoft.AspNetCore.Identity.RoleManager<IdentityRole<int>>(rm, null, null, null, null);
            }

        }

    }
}
