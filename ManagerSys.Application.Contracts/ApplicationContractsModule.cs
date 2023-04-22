using ManagerSys.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace ManagerSys.Application.Contracts
{
    // 依赖应用层约定模块
    // 依赖领域共享模块
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(DomainSharedModule)
    )]
    public class ApplicationContractsModule : AbpModule
    {

    }
}
