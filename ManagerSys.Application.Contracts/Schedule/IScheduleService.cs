using ManagerSys.Application.Contracts.Schedule.Dto;
using ManagerSys.Domain.Shared.Enums;
using ManagerSys.Domain.Shared.PageModel;
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
        Task<BasePage<ScheduleEntity>> Add(ScheduleAddDto model);

        /// <summary>
        /// 根据状态查询
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<List<ScheduleEntity>> QueryListByStatus(int? status);

        /// <summary>
        /// 根据guid 查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ScheduleEntity> GetScheduleById(Guid Id);

        /// <summary>
        /// 任务监听 
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="nextRunTime"></param>
        void StartedEvent(Guid scheduleId, DateTime? nextRunTime = null);


        /// <summary>
        /// 更新调度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ScheduleEntity> Update(ScheduleEntity model);
    }
}
