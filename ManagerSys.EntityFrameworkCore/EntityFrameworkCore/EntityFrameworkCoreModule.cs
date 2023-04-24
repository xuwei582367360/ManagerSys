using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerSys.EntityFrameworkCore
{
    // 依赖SqlServer数据库模块
    [DependsOn(
        typeof(AbpEntityFrameworkCoreSqlServerModule)
    )]
    public class EntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ManagerSysEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CDZLSDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            }
            );

            context.Services.AddAbpDbContext<CDZLSTestDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            }); ;

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                options.Configure(ctx =>
                {
                    if (ctx.ExistingConnection != null)
                    {
                        ctx.DbContextOptions.UseOracle(ctx.ExistingConnection);
                    }
                    else
                    {
                        ctx.DbContextOptions.UseOracle(ctx.ConnectionString);
                    }
                });
            });
        }
    }
}
