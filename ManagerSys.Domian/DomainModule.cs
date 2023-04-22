using ManagerSys.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ManagerSys.Domian
{
    // 依赖领域驱动模块
    // 依赖领域共享模块
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DomainSharedModule)
    )]
    public class DomainModule : AbpModule
    {

    }
}
