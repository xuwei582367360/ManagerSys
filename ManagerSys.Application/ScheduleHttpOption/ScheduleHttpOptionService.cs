using ManagerSys.Application.Contracts.Schedule.Dto;
using ManagerSys.Application.Contracts.ScheduleHttpOption;
using ManagerSys.Domian.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.ScheduleHttpOption
{
    public class ScheduleHttpOptionService : ApplicationAppService, IScheduleHttpOptionService
    {
        #region Identity
        private readonly IRepository<ScheduleEntity, Guid> _scheduleRepository;
        private readonly IRepository<ScheduleHttpOptionEntity, Guid> _scheduleHttpOptionRepository;
        public ScheduleHttpOptionService(IRepository<ScheduleEntity, Guid> scheduleRepository,
            IRepository<ScheduleHttpOptionEntity, Guid> scheduleHttpOptionRepository)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleHttpOptionRepository = scheduleHttpOptionRepository;
        }
        #endregion


        /// <summary>
        /// 根据guid 获取未删除的调度任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ScheduleHttpOptionEntity> GetHttpOptionByScheduleId(Guid scheduleId)
        {
            return (await _scheduleHttpOptionRepository.GetQueryableAsync())
                 .WhereIf(true, x => !x.IsDeleted)
                .WhereIf(true, x => x.ScheduleId == scheduleId)?.FirstOrDefault();
        }


        /// <summary>
        /// 新增http请求
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ScheduleEntity> Add(ScheduleAddDto model)
        {
            var scheModel = ObjectMapper.Map<ScheduleAddDto, ScheduleEntity>(model);
            return await _scheduleRepository.InsertAsync(scheModel);
        }
    }
}
