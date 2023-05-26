using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.QuartzNet
{
    /// <summary>
    /// IJob 对象无法构造注入 需要此类实现 返回 注入后得 Job 实例
    /// </summary>
    public class ResultfulApiJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ResultfulApiJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            //Job类型
            Type jobType = bundle.JobDetail.JobType;

            return _serviceProvider.GetService(jobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}

