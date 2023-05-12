using ManagerSys.Domain.Shared.ConfigHelper;
using ManagerSys.Domian;
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
    public class BusDbContextFactory : IDesignTimeDbContextFactory<BusDbContext>
    {
        public BusDbContext CreateDbContext(string[] args)
        {
            ManagerSysEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();
            //string dbTypeString = Config.ReadAppSettings("ConnectionStrings", "Provider");
            //string connectonString = Config.ReadAppSettings("ConnectionStrings", "Business");
            string dbTypeString = configuration.GetConnectionString("Provider");
            string connectonString = configuration.GetConnectionString("Business");
            DbContextOptionsBuilder<BusDbContext>? builder = null;
            switch (dbTypeString)
            {
                case "Orcal":
                    builder = new DbContextOptionsBuilder<BusDbContext>()
                .UseOracle(connectonString);
                    break;
                case "Sqlserver":
                    builder = new DbContextOptionsBuilder<BusDbContext>()
                .UseSqlServer(connectonString);
                    break;
                default:
                    builder = new DbContextOptionsBuilder<BusDbContext>()
               .UseSqlServer(connectonString);
                    break;
            }           
            return new BusDbContext(builder.Options);
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
