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
        public async Task<ScheduleEntity> GetScheduleById(Guid Id)
        {
            return (await _scheduleRepository.GetQueryableAsync())
                .WhereIf(true, x => x.Status != (int)ScheduleStatus.Deleted)
                 .WhereIf(true, x => !x.IsDeleted)
                .WhereIf(true, x => x.Id == Id)?.FirstOrDefault();
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