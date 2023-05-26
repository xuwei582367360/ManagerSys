using ManagerSys.Application.Contracts.Schedule;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.ScheduleHosted
{
    /// <summary>
    /// 初始化调度启动service
    /// </summary>
    public class ScheduleHostedService : ApplicationAppService, IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IHttpResultfulJobService _httpResultfulJobService;
        private readonly IJobExecutionContext context;
        public ScheduleHostedService(IHostApplicationLifetime appLifetime,
            IHttpResultfulJobService httpResultfulJobService)
        {
            _appLifetime = appLifetime;
            _httpResultfulJobService = httpResultfulJobService;
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



        private async void OnStarted()
        {
            //初始化Quartz
             _httpResultfulJobService.InitScheduler().Wait();


        }

    }



}
