using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Log
{
    public interface ISysLogService
    {
        /// <summary> 
        /// 新增日志信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Add(SysLogAddDto model);
    }
}
