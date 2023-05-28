using ManagerSys.Application;
using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Domian.Schedule;
using ManagerSys.Host.Schedule;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Host.AppStart
{
    /// <summary>
    /// 清理那些已经停止但是quartz里面还在运行的任务
    /// </summary>
    public class TaskClearJob : ApplicationAppService, IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskClearJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                if (scope != null)
                {
                    var service = scope.ServiceProvider.GetService<IScheduleService>();
                    var stoppedList = new List<ScheduleEntity>();
                    if (service != null)
                    {
                        stoppedList = await service.QueryListByStatus((int)ScheduleStatus.Stop);
                    }
                    foreach (var item in stoppedList)
                    {
                        JobKey jk = new JobKey(item.Id.ToString().ToLower());
                        if (await context.Scheduler.CheckExists(jk))
                        {
                            await ScheduleManager.Stop(item.Id);
                        }
                    }
                }
            }
        }
    }
}
