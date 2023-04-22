using ManagerSys.Application.Contracts;
using ManagerSys.Domian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace ManagerSys.Application
{
    // 依赖自动配置模块
    // 依赖领域驱动应用层模块
    // 依赖领域模块
    // 依赖应用层约定模块
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddApplicationModule),
        typeof(DomainModule),
        typeof(ApplicationContractsModule)
    )]
    [RemoteService(false)]
    public class ApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            // 配置自动映射
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ApplicationModule>();
            });
        }
    }
}
