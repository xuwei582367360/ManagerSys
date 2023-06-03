﻿using ManagerSys.Application;
using ManagerSys.Application.Contracts.Schedule;
using ManagerSys.Application.Contracts.ScheduleHttpOption;
using ManagerSys.Domain.Shared.QuartzNet;
using ManagerSys.Domian.HostSchedule;
using ManagerSys.Domian.Schedule;
using ManagerSys.HttpApi.AppStart;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using Serilog;
using System.Collections.Specialized;

namespace ManagerSys.HttpApi.Schedule
{
    public class ScheduleManager : ApplicationAppService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ResultfulApiJobFactory _resultfulApiJobFactory;
        public ScheduleManager(IServiceProvider serviceProvider, ResultfulApiJobFactory resultfulApiJobFactory)
        {
            _serviceProvider = serviceProvider;
            _resultfulApiJobFactory = resultfulApiJobFactory;
        }
        /// <summary>
        /// 调度器实例
        /// </summary>
        private static IScheduler _scheduler = null;


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
                Log.Information("任务调度平台初始化成功");
                //启动系统任务
                await Start<TaskClearJob>("task-clear", "0 0/1 * * * ? *");
                //恢复任务
                RunningRecovery();
            }
            catch (Exception ex)
            {
                Log.Information("任务调度平台初始化失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 关闭调度系统
        /// </summary>
        public static async Task Shutdown(bool isOnStop = false)
        {
            try
            {
                //判断调度是否已经关闭
                if (_scheduler != null && !_scheduler.IsShutdown)
                {
                    //等待任务运行完成再关闭调度
                    await _scheduler.Clear();
                    await _scheduler.Shutdown(true);
                    _scheduler = null;
                }
                Log.Information("任务调度平台已经停止！");
            }
            catch (Exception ex)
            {
                Log.Error("任务调度平台停止失败！", ex);
            }
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
            ScheduleOperation context = await GetScheduleContext(sid);
            IHosSchedule schedule = await HosScheduleFactory.GetHosSchedule(context);
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        await Start(schedule);
                        return true;
                    }
                    catch (SchedulerException sexp)
                    {
                        Log.Error($"任务启动失败！开始第{i + 1}次重试...", sexp, context.Schedule.Id);
                    }
                }
                //最后一次尝试
                await Start(schedule);
                return true;
            }
            catch (SchedulerException sexp)
            {
                Log.Error($"任务所有重试都失败了，已放弃启动！", sexp, context.Schedule.Id);
                return false;
            }
            catch (Exception exp)
            {
                Log.Error($"任务启动失败！", exp, context.Schedule.Id);
                return false;
            }
        }

        /// <summary>
        /// 暂停一个任务
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public async Task<bool> Pause(Guid sid)
        {
            try
            {
                JobKey jk = new JobKey(sid.ToString().ToLower());
                if (await _scheduler.CheckExists(jk))
                {
                    var jobDetail = await _scheduler.GetJobDetail(jk);

                    var instance = jobDetail.JobDataMap["instance"] as IHosSchedule;
                    CancellationToken token = instance == null ? default : instance.CancellationTokenSource.Token;

                    //任务已经存在则暂停任务
                    await _scheduler.PauseJob(jk, token);
                    if (jobDetail.JobType.GetInterface("IInterruptableJob") != null)
                    {
                        await _scheduler.Interrupt(jk, token);
                    }
                    //发送取消信号
                    instance?.CancellationTokenSource.Cancel();
                    Log.Warning($"任务已经暂停运行！", sid);
                    return true;
                }
                return false;
            }
            catch (Exception exp)
            {
                Log.Error($"任务暂停运行失败！", exp, sid);
                return false;
            }
        }

        /// <summary>
        /// 恢复运行
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public async Task<bool> Resume(Guid sid)
        {
            try
            {
                JobKey jk = new JobKey(sid.ToString().ToLower());
                if (await _scheduler.CheckExists(jk))
                {
                    var jobDetail = await _scheduler.GetJobDetail(jk);
                    var instance = jobDetail.JobDataMap["instance"] as IHosSchedule;

                    //重置token
                    instance.CancellationTokenSource = new CancellationTokenSource();
                    CancellationToken token = instance.CancellationTokenSource.Token;
                    instance.RunnableInstance.CancellationToken = token;

                    //恢复任务继续执行
                    await _scheduler.ResumeJob(jk, token);
                    Log.Information($"任务已经恢复运行！", sid);
                    return true;
                }
                return false;
            }
            catch (Exception exp)
            {
                Log.Error($"任务恢复运行失败！", exp, sid);
                return false;
            }
        }

        /// <summary>
        /// 停止一个任务
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public async Task<bool> Stop(Guid sid)
        {
            try
            {
                JobKey jk = new JobKey(sid.ToString().ToLower());
                var job = await _scheduler.GetJobDetail(jk);
                if (job != null)
                {
                    CancellationToken token = default;
                    var instance = job.JobDataMap["instance"] as IHosSchedule;
                    //释放资源
                    if (instance != null)
                    {
                        instance.RunnableInstance?.Dispose();
                        instance.Dispose();
                        token = instance.CancellationTokenSource.Token;
                    }
                    //删除quartz有关设置
                    var trigger = new TriggerKey(sid.ToString());
                    await _scheduler.PauseTrigger(trigger, token);
                    await _scheduler.UnscheduleJob(trigger, token);
                    await _scheduler.DeleteJob(jk, token);
                    _scheduler.ListenerManager.RemoveJobListener(sid.ToString());
                    //发送取消信号
                    instance?.CancellationTokenSource.Cancel();
                }
                Log.Information($"任务已经停止运行！", sid);
                return true;
            }
            catch (Exception exp)
            {
                Log.Error($"任务停止失败！", exp, sid);
                return false;
            }
        }

        /// <summary>
        ///立即运行一次任务
        /// </summary>
        /// <param name="sid"></param>
        public async Task<bool> RunOnce(Guid sid)
        {
            JobKey jk = new JobKey(sid.ToString().ToLower());
            if (await _scheduler.CheckExists(jk))
            {
                await _scheduler.TriggerJob(jk);
                return true;
            }
            else
            {
                Log.Error($"_scheduler.CheckExists=false", sid);
            }
            return false;
        }

        /// <summary>
        /// 执行自定义任务类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <param name="cronExp"></param>
        /// <returns></returns>
        public  async Task Start<T>(string identity, string cronExp) where T : IJob
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

        #region 私有方法

        private  async Task Start(IHosSchedule schedule)
        {
            JobDataMap map = new JobDataMap
            {
                new KeyValuePair<string, object> ("instance",schedule),
            };
            string jobKey = schedule.Schedule.Id.ToString();
            try
            {
                IJobDetail job = JobBuilder.Create().OfType(schedule.GetQuartzJobType()).WithIdentity(jobKey).UsingJobData(map).Build();

                //添加监听器
                var listener = new JobRunListener(jobKey);
                listener.OnSuccess += StartedEvent;
                _scheduler.ListenerManager.AddJobListener(listener, KeyMatcher<JobKey>.KeyEquals(new JobKey(jobKey)));

                ITrigger trigger = GetTrigger(schedule.Schedule);
                await _scheduler.ScheduleJob(job, trigger, schedule.CancellationTokenSource.Token);

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

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    //if (schedule.RunnableInstance == null) break;
                    //var log = schedule.RunnableInstance.ReadLog();
                    //if (log != null)
                    //{
                    //    LogManager.Queue.Write(new SystemLogEntity
                    //    {
                    //        Category = log.Category,
                    //        Message = log.Message,
                    //        ScheduleId = log.ScheduleId,
                    //        Node = log.Node,
                    //        StackTrace = log.StackTrace,
                    //        TraceId = log.TraceId,
                    //        CreateTime = log.CreateTime
                    //    });
                    //}
                    //else
                    //{
                    //    await Task.Delay(3000);
                    //}
                }
            });
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

        private async Task<ScheduleOperation> GetScheduleContext(Guid sid)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IScheduleService>();
                var optionService = scope.ServiceProvider.GetService<IScheduleHttpOptionService>();
                var entity = new ScheduleEntity();
                if (service != null)
                {
                    entity = await service.GetScheduleById(sid);
                }

                ScheduleOperation operation = new ScheduleOperation() { Schedule = entity };
                if (entity.MetaType == (int)ScheduleMetaType.Http)
                {
                    if (optionService != null)
                    {
                        operation.HttpOption = await optionService.GetHttpOptionByScheduleId(entity.Id);
                    }
                }
                return operation;
            }

            //using (var scope = new Core.ScopeDbContext())
            //{
            //    var db = scope.GetDbContext();
            //    var model = db.Schedules.FirstOrDefault(x => x.Id == sid && x.Status != (int)ScheduleStatus.Deleted);
            //    if (model != null)
            //    {
            //        ScheduleContext context = new ScheduleContext() { Schedule = model };
            //        if (model.MetaType == (int)ScheduleMetaType.Http)
            //        {
            //            context.HttpOption = db.ScheduleHttpOptions.FirstOrDefault(x => x.ScheduleId == sid);
            //        }
            //        context.Keepers = (from t in db.ScheduleKeepers
            //                           join u in db.SystemUsers on t.UserId equals u.Id
            //                           where t.ScheduleId == model.Id && !string.IsNullOrEmpty(u.Email)
            //                           select new KeyValuePair<string, string>(u.RealName, u.Email)
            //                ).ToList();
            //        context.Children = (from c in db.ScheduleReferences
            //                            join t in db.Schedules on c.ChildId equals t.Id
            //                            where c.ScheduleId == model.Id && c.ChildId != model.Id
            //                            select new { t.Id, t.Title }
            //                        ).ToDictionary(x => x.Id, x => x.Title);
            //        return context;
            //    }
            //    throw new InvalidOperationException($"不存在的任务id：{sid}");
            //}
        }


        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="nextRunTime"></param>
        private void StartedEvent(Guid scheduleId, DateTime? nextRunTime)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scheduleService = scope.ServiceProvider.GetService<IScheduleService>();
                scheduleService.StartedEvent(scheduleId);
            }
        }

        private async void RunningRecovery()
        {
            //任务恢复：查找那些绑定了本节点并且在状态是运行中的任务执行启动操作
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IScheduleService>();
                var runningList = new List<ScheduleEntity>();
                if (service != null)
                {
                    runningList = await service.QueryListByStatus((int)ScheduleStatus.Running);
                }
                runningList.AsParallel().ForAll(async sid =>
                {
                    try { await StartWithRetry(sid.Id); } catch { }
                });
            }
            //var list  = await _scheduleService.QueryListByStatus((int)ScheduleStatus.Running);
        }


        #endregion

        #region 邮件模板

        public static string GetErrorEmailContent(string sname, Exception ex)
        {
            string EmailTemplate = "<div style=\"background-color:#d0d0d0;text-align:center;padding:40px;font-family:'微软雅黑','黑体', 'Lucida Grande', Verdana, sans-serif;\"><div style=\"width:700px;margin:0 auto;padding:10px;color:#333;background-color:#fff;border:0px solid #aaa;border-radius:5px;-webkit-box-shadow:3px 3px 10px #999;-moz-box-shadow:3px 3px 10px #999;box-shadow:3px 3px 10px #999;font-family:Verdana, sans-serif; \"><style> .mmsgLetterContent p { margin:20px 0; padding:0; } 	.mmsgLetterContent 	{background: url(https://imgkr.cn-bj.ufileos.com/15e4202a-3221-4af7-a315-b55220a7e119.png) no-repeat top right; }</style><div class=\"mmsgLetterContent\" style=\"text-align:left;padding:30px;font-size:14px;line-height:1.5;\"><p>你好!</p><p>感谢你使用ScheduleMaster平台。 <br />你参与的任务<strong>$NAME$</strong>在[$TIME$] 运行发生异常，请及时查看处理。</p><p><span style = \"border-radius: 2px;background: linear-gradient(to right,#57b5e3,#c4e6f6) ;padding: 4px 6px 4px 6px;display: inline-block;line-height: 1;color: #fff;text-align: center;white-space: nowrap;vertical-align: baseline;\" > 错误信息 </span ><br /> <strong> $MESSAGE$ </strong></p><p><span style= \"border-radius: 2px ;background: linear-gradient(to right,#d73d32,#f7b5b0) ;padding: 4px 6px 4px 6px;display: inline-block;line-height: 1;color: #fff;text-align: center;white-space: nowrap;vertical-align: baseline;\" > 程序堆栈 </span><br /><span style= \"font-family: Consolas,'Courier New',Courier,FreeMono,monospace;\" >$STACKREACE$</span></p></div></div></div> ";
            return EmailTemplate.Replace("$NAME$", sname).Replace("$TIME$", DateTime.Now.ToString()).Replace("$MESSAGE$", ex.Message).Replace("$STACKREACE$", ex.StackTrace);
        }
        #endregion
    }

    /// <summary>
    /// 任务运行状态监听器
    /// </summary>
    internal class JobRunListener : IJobListener
    {
        public delegate void SuccessEventHandler(Guid sid, DateTime? nextTime);

        public string Name { get; set; }
        public event SuccessEventHandler OnSuccess;


        public JobRunListener()
        {
        }

        public JobRunListener(string name)
        {
            Name = name;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken)
        {
            IJobDetail job = context.JobDetail;
            var instance = job.JobDataMap["instance"] as IHosSchedule;

            if (jobException == null)
            {
                var utcDate = context.Trigger.GetNextFireTimeUtc();
                DateTime? nextTime = utcDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(utcDate.Value.DateTime, TimeZoneInfo.Local) : new DateTime?();
                OnSuccess(Guid.Parse(job.Key.Name), nextTime);

                ////子任务触发
                //Task.Run(async () =>
                //{
                //    foreach (var item in instance.Children)
                //    {
                //        var jobkey = new JobKey(item.Key.ToString());
                //        if (await context.Scheduler.CheckExists(jobkey))
                //        {
                //            JobDataMap map = new JobDataMap{
                //                 new KeyValuePair<string, object>("PreviousResult", context.Result)
                //             };
                //            await context.Scheduler.TriggerJob(jobkey, map);
                //        }
                //    }
                //});
            }
            else if (jobException is BusinessRunException)
            {
                Task.Run(() =>
                {
                    //if (instance.Keepers != null && instance.Keepers.Any())
                    //{
                    //    MailKitHelper.SendMail(
                    //        instance.Keepers,
                    //        $"任务运行异常 — {instance.Main.Title}",
                    //        QuartzManager.GetErrorEmailContent(instance.Main.Title, (jobException as BusinessRunException).Detail)
                    //     );
                    //}
                });
            }
            return Task.FromResult(0);
        }
    }

    public class BusinessRunException : JobExecutionException
    {
        public Exception Detail;
        public BusinessRunException(Exception exp)
        {
            Detail = exp;
        }
    }

}
