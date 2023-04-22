using ManagerSys.Domain.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace ManagerSys.Application
{
    /// <summary>
    /// 应用层服务基类
    /// </summary>
    public abstract class ApplicationAppService : ApplicationService
    {
        protected ApplicationAppService()
        {
            // 配置本地资源
            LocalizationResource = typeof(MyLocalizationResource);
        }
    }
}
