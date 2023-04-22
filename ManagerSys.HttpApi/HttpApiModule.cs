using Localization.Resources.AbpUi;
using ManagerSys.Application.Contracts;
using ManagerSys.Domain.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace ManagerSys.HttpApi
{

    // 依赖MVC模块
    // 依赖应用约定模块
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(ApplicationContractsModule)
    )]
    public class HttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
            base.ConfigureServices(context);
        }

        private void ConfigureLocalization()
        {
            // 配置本地化资源库
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MyLocalizationResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
