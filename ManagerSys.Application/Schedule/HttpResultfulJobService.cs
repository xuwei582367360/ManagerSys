using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Application.Contracts.Schedule.Dto;
using ManagerSys.Domain.Shared.Http;
using ManagerSys.Domain.Shared.QuartzNet;
using ManagerSys.Domian.Schedule;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.Schedule
{
    public class HttpResultfulJobService : ApplicationAppService, IHttpResultfulJobService
    {
        /// <summary>
        /// 调度器实例
        /// </summary>
        private static IScheduler _scheduler = null;

        private readonly IRepository<ScheduleEntity, Guid> _scheduleRepository;
        private readonly ResultfulApiJobFactory _resultfulApiJobFactory;
        public HttpResultfulJobService(IScheduleService scheduleService,
            IRepository<ScheduleEntity, Guid> scheduleRepository, ResultfulApiJobFactory resultfulApiJobFactory)
        {
            _scheduleRepository = scheduleRepository;
            _resultfulApiJobFactory = resultfulApiJobFactory;
        }

        /// <summary>
        /// 初始化调度系统
        /// </summary>
        public async Task InitScheduler()
        {
            try
            {
                if (_scheduler == null || _scheduler.IsShutdown)
                {
                    NameValueCollection properties = new NameValueCollection();
                    properties["quartz.scheduler.instanceName"] = "Hos.ScheduleMaster";
                    //int maxConcurrency = ConfigurationCache.NodeSetting.MaxConcurrency;
                    //if (maxConcurrency > 0)
                    //{
                    //    properties["quartz.threadPool.maxConcurrency"] = maxConcurrency.ToString();
                    //}

                    ISchedulerFactory factory = new StdSchedulerFactory(properties);
                    _scheduler = await factory.GetScheduler();
                    //4、写入 Job 实例工厂 解决 Job 中取 ioc 对象
                    _scheduler.JobFactory = _resultfulApiJobFactory;
                }
                await _scheduler.Start();
                await _scheduler.Clear();
                //MarkNode(true);
                //LogHelper.Info("任务调度平台初始化成功！");
                //启动系统任务
                await Start<TaskClearJob>("task-clear", "0 0/1 * * * ? *");
                var runningList = (await _scheduleRepository.GetQueryableAsync()).WhereIf(true, s => s.Status == (int)ScheduleStatus.Running).Select(q => q.Id).ToList();
                //恢复任务
                RunningRecovery(runningList);
            }
            catch (Exception ex)
            {
                //LogHelper.Error("任务调度平台初始化失败！", ex);
            }
        }


        /// <summary>
        /// 重启任务
        /// </summary>
        public async void RunningRecovery(List<Guid> runningList)
        {
            //任务恢复：查找那些状态是运行中的任务执行启动操作

            runningList.AsParallel().ForAll(async sid =>
            {
                //try { await StartWithRetry(sid); } catch { }
            });
        }

        /// <summary>
        /// 启动一个任务，带重试机制
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        //public static async Task<bool> StartWithRetry(Guid sid)
        //{
        //    var jk = new JobKey(sid.ToString().ToLower());
        //    if (await _scheduler.CheckExists(jk))
        //    {
        //        return true;
        //    }
        //    ScheduleContext context = GetScheduleContext(sid);
        //    IHosSchedule schedule = await HosScheduleFactory.GetHosSchedule(context);
        //    try
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            try
        //            {
        //                await Start(schedule);
        //                return true;
        //            }
        //            catch (SchedulerException sexp)
        //            {
        //                //LogHelper.Error($"任务启动失败！开始第{i + 1}次重试...", sexp, context.Schedule.Id);
        //            }
        //        }
        //        //最后一次尝试
        //        await Start(schedule);
        //        return true;
        //    }
        //    catch (SchedulerException sexp)
        //    {
        //       // LogHelper.Error($"任务所有重试都失败了，已放弃启动！", sexp, context.Schedule.Id);
        //        return false;
        //    }
        //    catch (Exception exp)
        //    {
        //        //LogHelper.Error($"任务启动失败！", exp, context.Schedule.Id);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 停止一个任务
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static async Task<bool> Stop(Guid sid)
        {
            try
            {
                JobKey jk = new JobKey(sid.ToString().ToLower());
                var job = await _scheduler.GetJobDetail(jk);
                if (job != null)
                {
                    CancellationToken token = default;
                    //var instance = job.JobDataMap["instance"] as IHosSchedule;
                    ////释放资源
                    //if (instance != null)
                    //{
                    //    instance.RunnableInstance?.Dispose();
                    //    instance.Dispose();
                    //    token = instance.CancellationTokenSource.Token;
                    //}
                    //删除quartz有关设置
                    var trigger = new TriggerKey(sid.ToString());
                    await _scheduler.PauseTrigger(trigger, token);
                    await _scheduler.UnscheduleJob(trigger, token);
                    await _scheduler.DeleteJob(jk, token);
                    _scheduler.ListenerManager.RemoveJobListener(sid.ToString());
                    //发送取消信号
                    //instance?.CancellationTokenSource.Cancel();
                }
                //LogHelper.Info($"任务已经停止运行！", sid);
                return true;
            }
            catch (Exception exp)
            {
                //LogHelper.Error($"任务停止失败！", exp, sid);
                return false;
            }
        }


        /// <summary>
        /// 执行自定义任务类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <param name="cronExp"></param>
        /// <returns></returns>
        public static async Task Start<T>(string identity, string cronExp) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>().WithIdentity(identity).Build();
            CronTriggerImpl trigger = new CronTriggerImpl
            {
                CronExpressionString = cronExp,
                Name = identity,
                Key = new TriggerKey(identity)
            };
            trigger.StartTimeUtc = DateTimeOffset.Now;
            await _scheduler.ScheduleJob(job, trigger);
        }


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
                    var service = scope.ServiceProvider.GetService<IScheduleService>();
                    var stoppedList = new List<ScheduleEntity>();
                    if (service != null)
                    {
                        stoppedList = await service.QueryStopList();
                    }
                    foreach (var item in stoppedList)
                    {
                        JobKey jk = new JobKey(item.Id.ToString().ToLower());
                        if (await context.Scheduler.CheckExists(jk))
                        {
                            await Stop(item.Id);
                        }
                    }
                }
                //using (var scope = new ScopeDbContext())
                //{
                //    var _db = scope.GetDbContext();
                //    var stoppedList = (from s in _db.Schedules
                //                       join n in _db.ScheduleExecutors on s.Id equals n.ScheduleId
                //                       where s.Status == (int)ScheduleStatus.Stop && n.WorkerName == ConfigurationCache.NodeSetting.IdentityName
                //                       select n.ScheduleId
                //                       ).ToList();
                //    foreach (var sid in stoppedList)
                //    {
                //        JobKey jk = new JobKey(sid.ToString().ToLower());
                //        if (await context.Scheduler.CheckExists(jk))
                //        {
                //            await QuartzManager.Stop(sid);
                //        }
                //    }
                //}
            }
        }
    }
}
