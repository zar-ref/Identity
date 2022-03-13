using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.DataAccess.DbContexts.Dev2
{
    public class Dev2Context : BaseContext
    {
        public Dev2Context(DbContextOptions<Dev2Context> options) : base(options)
        {
            ContextName = "Dev2";
        }

        //tutorial: https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Dev2Context>
        {
            public Dev2Context CreateDbContext(string[] args)
            {

                var builder = new DbContextOptionsBuilder<Dev2Context>();
                var connectionString = "Server=localhost;Database=Identity_Dev_main;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;";
                builder.UseSqlServer(connectionString);
                return new Dev2Context(builder.Options);
            }
        }

        // commands on Infrastructure main folder:
        //  dotnet ef migrations add Initial --context Identity.Infrastructure.DataAccess.DbContexts.Dev2.Dev2Context -o D:\Identity\src\Identity.Infrastructure\DataAccess\DbContexts\Dev2\Migrations
        //  dotnet ef database update --context Dev2Context
    }
}
