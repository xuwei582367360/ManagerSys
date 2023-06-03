using ManagerSys.Application;
using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Application.Contracts.ScheduleHttpOption;
using ManagerSys.Domain.Shared.QuartzNet;
using ManagerSys.Domian.HostSchedule;
using ManagerSys.Domian.Schedule;
using ManagerSys.HttpApi.Schedule;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using Serilog;
using System.Collections.Specialized;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.HttpApi.AppStart
{
    public class HttpResultfulJobService : ApplicationAppService
    {
        /// <summary>
        /// 调度器实例
        /// </summary>
        private static IScheduler _scheduler = null;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<ScheduleEntity, Guid> _scheduleRepository;
        private readonly ResultfulApiJobFactory _resultfulApiJobFactory;
        public HttpResultfulJobService(IScheduleService scheduleService,
            IRepository<ScheduleEntity, Guid> scheduleRepository, ResultfulApiJobFactory resultfulApiJobFactory,
            IServiceProvider serviceProvider)
        {
            _scheduleRepository = scheduleRepository;
            _resultfulApiJobFactory = resultfulApiJobFactory;
            _serviceProvider = serviceProvider;
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
                try { await StartWithRetry(sid); } catch { }
            });
        }

        /// <summary>
        /// 启动一个任务，带重试机制
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> StartWithRetry(Guid sid)
        {
            var jk = new JobKey(sid.ToString().ToLower());
            if (await _scheduler.CheckExists(jk))
            {
                return true;
            }
            ScheduleOperation schedule = await GetScheduleContext(sid);
            IHosSchedule hostSchedule = await HosScheduleFactory.GetHosSchedule(schedule);
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        await Start(hostSchedule);
                        return true;
                    }
                    catch (SchedulerException sexp)
                    {
                        Log.Error($"任务启动失败！开始第{i + 1}次重试...", sexp, schedule.Schedule.Id);
                    }
                }
                //最后一次尝试
                await Start(hostSchedule);
                return true;
            }
            catch (SchedulerException sexp)
            {
                Log.Error($"任务所有重试都失败了，已放弃启动！", sexp, schedule.Schedule.Id);
                return false;
            }
            catch (Exception exp)
            {
                Log.Error($"任务启动失败！", exp, schedule.Schedule.Id);
                return false;
            }
        }


        private async Task<ScheduleOperation> GetScheduleContext(Guid sid)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetService<IScheduleService>();
                var httpService = scope.ServiceProvider.GetService<IScheduleHttpOptionService>();
                var model = await scheduleService.GetScheduleById(sid);

                if (model != null)
                {
                    ScheduleOperation context = new ScheduleOperation() { Schedule = model };
                    if (model.MetaType == (int)ScheduleMetaType.Http)
                    {
                        context.HttpOption = await httpService.GetHttpOptionByScheduleId(sid);
                    }
                    //context.Keepers = (from t in db.ScheduleKeepers
                    //                   join u in db.SystemUsers on t.UserId equals u.Id
                    //                   where t.ScheduleId == model.Id && !string.IsNullOrEmpty(u.Email)
                    //                   select new KeyValuePair<string, string>(u.RealName, u.Email)
                    //        ).ToList();
                    //context.Children = (from c in db.ScheduleReferences
                    //                    join t in db.Schedules on c.ChildId equals t.Id
                    //                    where c.ScheduleId == model.Id && c.ChildId != model.Id
                    //                    select new { t.Id, t.Title }
                    //                ).ToDictionary(x => x.Id, x => x.Title);
                    return context;
                }
                throw new InvalidOperationException($"不存在的任务id：{sid}");
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
        /// 执行任务
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        /// <exception cref="SchedulerException"></exception>
        private async Task Start(IHosSchedule schedule)
        {
            JobDataMap map = new JobDataMap
            {
                new KeyValuePair<string, object> ("instance",schedule),
            };
            string jobKey = schedule.Schedule.Id.ToString();
            try
            {
                IJobDetail job = JobBuilder.Create()/*.OfType(schedule.GetQuartzJobType())*/.WithIdentity(jobKey).UsingJobData(map).Build();
                #region 添加任务监听器
                var listener = new JobRunListener(jobKey);
                listener.OnSuccess += StartedEvent;
                _scheduler.ListenerManager.AddJobListener(listener, KeyMatcher<JobKey>.KeyEquals(new JobKey(jobKey)));
                #endregion

                ITrigger trigger = GetTrigger(schedule.Schedule);
                await _scheduler.ScheduleJob(job, trigger, default);

                //更新下次运行时间
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scheduleService = scope.ServiceProvider.GetService<IScheduleService>();
                    var scheEntity = await scheduleService.GetScheduleById(schedule.Schedule.Id);
                    if (scheEntity != null)
                    {
                        scheEntity.NextRunTime = TimeZoneInfo.ConvertTimeFromUtc(trigger.GetNextFireTimeUtc().Value.UtcDateTime, TimeZoneInfo.Local);
                        await scheduleService.Update(scheEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SchedulerException(ex);
            }
            Log.Information($"任务[{schedule.Schedule.Title}]启动成功！", schedule.Schedule.Id);

            //_ = Task.Run(async () =>
            //{
            //    while (true)
            //    {
            //        if (schedule.RunnableInstance == null) break;
            //        var log = schedule.RunnableInstance.ReadLog();
            //        if (log != null)
            //        {
            //            LogManager.Queue.Write(new SystemLogEntity
            //            {
            //                Category = log.Category,
            //                Message = log.Message,
            //                ScheduleId = log.ScheduleId,
            //                Node = log.Node,
            //                StackTrace = log.StackTrace,
            //                TraceId = log.TraceId,
            //                CreateTime = log.CreateTime
            //            });
            //        }
            //        else
            //        {
            //            await Task.Delay(3000);
            //        }
            //    }
            //});
        }

        private void StartedEvent(Guid scheduleId, DateTime? nextRunTime)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetService<IScheduleService>();
                scheduleService.StartedEvent(scheduleId);
            }
        }

        private static ITrigger GetTrigger(ScheduleEntity model)
        {
            string jobKey = model.Id.ToString();
            if (model.RunLoop)
            {
                if (!CronExpression.IsValidExpression(model.CronExpression))
                {
                    throw new Exception("cron表达式验证失败");
                }
                CronTriggerImpl trigger = new CronTriggerImpl
                {
                    CronExpressionString = model.CronExpression,
                    Name = model.Title,
                    Key = new TriggerKey(jobKey),
                    Description = model.Remark,
                    MisfireInstruction = MisfireInstruction.CronTrigger.DoNothing
                };
                if (model.StartDate.HasValue)
                {
                    if (model.StartDate.Value < DateTime.Now) model.StartDate = DateTime.Now;
                    trigger.StartTimeUtc = TimeZoneInfo.ConvertTimeToUtc(model.StartDate.Value);
                }
                if (model.EndDate.HasValue)
                {
                    trigger.EndTimeUtc = TimeZoneInfo.ConvertTimeToUtc(model.EndDate.Value);
                }
                return trigger;
            }
            else
            {
                DateTimeOffset start = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
                if (model.StartDate.HasValue)
                {
                    start = TimeZoneInfo.ConvertTimeToUtc(model.StartDate.Value);
                }
                DateTimeOffset end = start.AddMinutes(1);
                if (model.EndDate.HasValue)
                {
                    end = TimeZoneInfo.ConvertTimeToUtc(model.EndDate.Value);
                }
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(jobKey)
                    .StartAt(start)
                    .WithSimpleSchedule(x => x
                    .WithRepeatCount(1).WithIntervalInMinutes(1))
                    .EndAt(end)
                    .Build();
                return trigger;
            }
        }


    }
}
