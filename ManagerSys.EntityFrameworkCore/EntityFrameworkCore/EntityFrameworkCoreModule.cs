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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ManagerSys.Domian;
using ManagerSys.EntityFrameworkCore.EntityFrameworkCore;
using ManagerSys.Domain.Shared.ConfigHelper;

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
            context.Services.AddAbpDbContext<ScheDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            }
            );

            context.Services.AddAbpDbContext<BusDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);//
            });

            Configure<AbpDbContextOptions>(options =>
            {
                string dbTypeString = Config.ReadAppSettings("ConnectionStrings", "Provider") ?? "sqlserver";
                options.Configure(ctx =>
                {
                    if (ctx.ExistingConnection != null)
                    {
                        //ctx.DbContextOptions.UseSqlServer(ctx.ExistingConnection);
                        ManagerSysContextConfigurer.Configure(ctx, dbTypeString, ctx.ExistingConnection);
                    }
                    else
                    {
                        //ctx.DbContextOptions.UseSqlServer(ctx.ConnectionString);
                        ManagerSysContextConfigurer.Configure(ctx, dbTypeString, ctx.ConnectionString);
                    }
                });
            });
        }
    }
}
