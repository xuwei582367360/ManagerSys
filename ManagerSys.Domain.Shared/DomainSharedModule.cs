using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using ManagerSys.Domain.Shared.Localization;

namespace ManagerSys.Domain.Shared
{
    // 添加依赖  本地化模组
    [DependsOn(
        typeof(AbpLocalizationModule)
    )]
    public class DomainSharedModule : AbpModule // 必须继承AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 配置虚拟文件系统  配置全球化资源文件必须配置该选项
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DomainSharedModule>();
            });

            // 配置全球化资源文件
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<MyLocalizationResource>("zh-Hans")
                .AddVirtualJson("/Localization/MyLocalization");

                options.DefaultResourceType = typeof(MyLocalizationResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("MyLocalization", typeof(MyLocalizationResource));
            });
            base.ConfigureServices(context);
        }
    }
}
