using ManagerSys.Domian;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;

namespace ManagerSys.EntityFrameworkCore.EntityFrameworkCore
{
    public class ManagerSysContextConfigurer
    {
        public static void Configure(AbpDbContextConfigurationContext ctx, string dbTypeString, string connectionString)
        {
            switch (dbTypeString)
            {
                case "Orcal":
                    ctx.DbContextOptions.UseOracle(connectionString);
                    break;
                case "Sqlserver":
                    ctx.DbContextOptions.UseSqlServer(connectionString);
                    break;
                default:
                    ctx.DbContextOptions.UseSqlServer(connectionString);
                    break;
            }
        }
        public static void Configure(AbpDbContextConfigurationContext ctx, string dbTypeString, DbConnection existingConnection)
        {

            switch (dbTypeString)
            {
                case "Orcal":
                    ctx.DbContextOptions.UseOracle(existingConnection);
                    break;
                case "Sqlserver":
                    ctx.DbContextOptions.UseSqlServer(existingConnection);
                    break;
                default:
                    ctx.DbContextOptions.UseSqlServer(existingConnection);
                    break;
            }
        }

    }
}
