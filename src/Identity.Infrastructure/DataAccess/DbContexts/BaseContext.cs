using Identity.Entities.Entities;
using Identity.Entities.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess.DbContexts
{
    public abstract class BaseContext : IdentityDbContext<Account, IdentityRole<int>, int >
    {

        public BaseContext(DbContextOptions options) : base(options) { }
        public string ContextName { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public UserManager<Account> UserManager;
        public RoleManager<IdentityAccountRole> RoleManager;

        //public Microsoft.EntityFrameworkCore.DbSet<Account> Accounts { get; set; }
        //public Microsoft.EntityFrameworkCore.DbSet<IdentityRole<int>> Accounts { get; set; }
        //public Microsoft.EntityFrameworkCore.DbSet<IdentityAccountRole> Roles { get; set; }
    }
}
