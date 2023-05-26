using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Schedule
{
    public interface IHttpResultfulJobService
    {
        Task InitScheduler();
    }
}
