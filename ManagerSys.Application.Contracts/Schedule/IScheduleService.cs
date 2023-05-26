using ManagerSys.Application.Contracts.Schedule.Dto;
using ManagerSys.Domain.Shared.Enums;
using ManagerSys.Domian.Schedule;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Schedule
{
    public interface IScheduleService
    {
        /// <summary> 
        /// 新增调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ScheduleEntity> Add(ScheduleAddDto model);

        Task<List<ScheduleEntity>> QueryStopList();
    }
}
