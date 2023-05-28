using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Application.Schedule;
using ManagerSys.Host.Schedule;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Host.AppStart
{
    /// <summary>
    /// 初始化调度启动service
    /// </summary>
    public class ScheduleHostedService :  IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IJobExecutionContext context;
        private readonly ScheduleManager _scheduleManager;
        public ScheduleHostedService(IHostApplicationLifetime appLifetime, ScheduleManager scheduleManager)
        {
            _appLifetime = appLifetime;
            _scheduleManager = scheduleManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }



        private  void OnStarted()
        {
            //初始化Quartz
            _scheduleManager.InitScheduler().Wait();

        }

    }



}
