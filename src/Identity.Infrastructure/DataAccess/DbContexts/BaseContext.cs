using Identity.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess.DbContexts
{
    public abstract class BaseContext : DbContext
    {

        public BaseContext(DbContextOptions options) : base(options) { }
        public string ContextName { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserRole> UserRoles { get; set; }
    }
}
