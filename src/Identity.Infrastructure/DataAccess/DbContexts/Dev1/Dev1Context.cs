using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess.DbContexts.Dev1
{
    public class Dev1Context : BaseContext
    {

        public Dev1Context(DbContextOptions<Dev1Context> options) : base(options)
        {
            ContextName = "Dev1";
        }

    }
    //tutorial: https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Dev1Context>
    {
        public Dev1Context CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<Dev1Context>();
            var connectionString = "Server=localhost;Database=Identity_Dev1;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;";
            builder.UseSqlServer(connectionString);
            return new Dev1Context(builder.Options);
        }
    }
    // commands for migrations to be done on infrastructure project (main folder):
    //  dotnet ef migrations add InitialCreate  --namespace DataAccess.DbContexts.Dev1.Migrations  --context Identity.Infrastructure.DataAccess.DbContexts.Dev1.Dev1Context --output-dir D:\Identity\src\Identity.Infrastructure\DataAccess\DbContexts\Dev1\Migrations --verbose
    //  dotnet ef database update --context Identity.Infrastructure.DataAccess.DbContexts.Dev1.Dev1Context


}
