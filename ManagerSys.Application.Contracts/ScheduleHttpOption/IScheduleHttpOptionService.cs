using ManagerSys.Domian.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.ScheduleHttpOption
{
    public interface IScheduleHttpOptionService
    {
        Task<ScheduleHttpOptionEntity> GetHttpOptionByScheduleId(Guid scheduleId);
    }
}
