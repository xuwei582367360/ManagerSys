using AutoMapper;
using ManagerSys.Application.Contracts.Log;
using ManagerSys.Domian.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ManagerSys.Application
{
    [RemoteService(false)]
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            // 配置自动映射
            CreateMap<SysLogAddDto, SysLog>();
        }
    }
}
