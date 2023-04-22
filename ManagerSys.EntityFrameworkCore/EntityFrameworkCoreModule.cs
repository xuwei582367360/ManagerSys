using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ManagerSys.EntityFrameworkCore
{
    // 依赖SqlServer数据库模块
    [DependsOn(
        typeof(AbpEntityFrameworkCoreSqlServerModule)
    )]
    public class EntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            // 配置使用 SqlServer
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
        }
    }
}
