using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Domian.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Quartz;
using Quartz.Spi;
using ManagerSys.Domain.Shared.QuartzNet;
using ManagerSys.Application.Contracts.Schedule.Dto;
using Quartz.Impl.Triggers;
using Quartz.Impl;
using Volo.Abp.Uow;
using Quartz.Impl.Matchers;
using ManagerSys.Domain.Shared.Enums;
using System.Security.Cryptography;
using LogDashboard.Repository;
using ManagerSys.Domain.Shared.PageModel;

namespace ManagerSys.Application.Schedule
{
    public class ScheduleService : ApplicationAppService, IScheduleService
    {
        #region Identity
        private readonly IRepository<ScheduleEntity, Guid> _scheduleRepository;
        private readonly IRepository<ScheduleHttpOptionEntity, Guid> _scheduleHttpOptionRepository;
        public ScheduleService(IRepository<ScheduleEntity, Guid> scheduleRepository,
            IRepository<ScheduleHttpOptionEntity, Guid> scheduleHttpOptionRepository)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleHttpOptionRepository = scheduleHttpOptionRepository;
        }
        #endregion


        /// <summary>
        /// 新增调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ScheduleEntity>> QueryList(ScheduleAddDto model = null)
        {
            return await _scheduleRepository.GetListAsync();
        }


        /// <summary>
        /// 根据状态返回任务
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleEntity>> QueryListByStatus(int? status)
        {
            return (await _scheduleRepository.GetQueryableAsync()).WhereIf(status != null, s => s.Status == status).ToList();
        }

        /// <summary>
        /// 根据guid 获取未删除的调度任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ScheduleEntity> GetScheduleById(Guid scheduleId)
        {
            return (await _scheduleRepository.GetQueryableAsync())
                .WhereIf(true, x => x.Status != (int)ScheduleStatus.Deleted)
                 .WhereIf(true, x => !x.IsDeleted)
                .WhereIf(true, x => x.Id == scheduleId)?.FirstOrDefault();
        }

        /// <summary>
        /// 新增调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [UnitOfWork]
        public async Task<BasePage<ScheduleEntity>> Add(ScheduleAddDto model)
        {
            var basePage = new BasePage<ScheduleEntity>() { Code = 200, Message = "添加成功" };
            var scheModel = ObjectMapper.Map<ScheduleAddDto, ScheduleEntity>(model);
            scheModel.Status = (int)ScheduleStatus.Stop;
            var scheEntity = await _scheduleRepository.InsertAsync(scheModel);
            var optionEntity = ObjectMapper.Map<ScheduleAddDto, ScheduleHttpOptionEntity>(model);
            optionEntity.ScheduleId = scheEntity.Id;
            await _scheduleHttpOptionRepository.InsertAsync(optionEntity);
            return basePage;
        }


        /// <summary>
        /// 修改调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ScheduleEntity> Update(ScheduleEntity model)
        {
            return await _scheduleRepository.UpdateAsync(model);
        }


        /// <summary>
        /// 删除调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async void Delete(Guid scheduleId)
        {
            await _scheduleRepository.DeleteAsync(scheduleId);
        }


        /// <summary>
        /// 任务监听
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="nextRunTime"></param>
        public async void StartedEvent(Guid scheduleId, DateTime? nextRunTime = null)
        {
            //每次运行成功后更新任务的运行情况
            var task = await _scheduleRepository.GetAsync(x => x.Id == scheduleId);
            if (task == null) return;
            task.LastRunTime = DateTime.Now;
            task.NextRunTime = nextRunTime;
            task.TotalRunCount += 1;
            await _scheduleRepository.UpdateAsync(task);
        }
    }
}