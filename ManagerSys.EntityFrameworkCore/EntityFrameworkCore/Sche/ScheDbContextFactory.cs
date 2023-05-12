using ManagerSys.Domain.Shared.ConfigHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
    public class ScheDbContextFactory : IDesignTimeDbContextFactory<ScheDbContext>
    {
        public ScheDbContext CreateDbContext(string[] args)
        {
            ManagerSysEfCoreEntityExtensionMappings.Configure();

            //string dbTypeString = Config.ReadAppSettings("ConnectionStrings", "Provider");
            //string connectonString = Config.ReadAppSettings("ConnectionStrings", "Default");
            var configuration = BuildConfiguration();
            string dbTypeString = configuration.GetConnectionString("Provider");
            string connectonString = configuration.GetConnectionString("Default");
            DbContextOptionsBuilder<ScheDbContext>? builder = null;
            switch (dbTypeString)
            {
                case "Orcal":
                    builder = new DbContextOptionsBuilder<ScheDbContext>()
                .UseOracle(connectonString);
                    break;
                case "Sqlserver":
                    builder = new DbContextOptionsBuilder<ScheDbContext>()
                .UseSqlServer(connectonString);
                    break;
                default:
                    builder = new DbContextOptionsBuilder<ScheDbContext>()
               .UseSqlServer(connectonString);
                    break;
            }
            return new ScheDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ABPManage.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
