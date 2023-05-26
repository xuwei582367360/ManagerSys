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
        /// 返回所有停止的任务
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleEntity>> QueryStopList()
        {
           return (await _scheduleRepository.GetQueryableAsync()).WhereIf(true, s => s.Status == (int)ScheduleStatus.Stop).ToList();
        }
        

        /// <summary>
        /// 新增调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [UnitOfWork]
        public async Task<ScheduleEntity> Add(ScheduleAddDto model)
        {
            var scheModel = ObjectMapper.Map<ScheduleAddDto, ScheduleEntity>(model);
            return await _scheduleRepository.InsertAsync(scheModel);
        }


        /// <summary>
        /// 新增调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [UnitOfWork]
        public async Task<ScheduleEntity> Update(ScheduleAddDto model)
        {
            var scheModel = ObjectMapper.Map<ScheduleAddDto, ScheduleEntity>(model);
            return await _scheduleRepository.UpdateAsync(scheModel);
        }


        /// <summary>
        /// 新增调度任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [UnitOfWork]
        public async void Delete(ScheduleAddDto model)
        {
            var scheModel = ObjectMapper.Map<ScheduleAddDto, ScheduleEntity>(model);
            await _scheduleRepository.DeleteAsync(scheModel);
        }
    }
}