using ManagerSys.Domain.Shared.QuartzNet;
using ManagerSys.Domain.Shared.QuartzNet.Base;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian.HostSchedule.Jobs
{
    /// <summary>
    /// http任务的入口
    /// by hoho
    /// </summary>
    public class HttpJob : RootJob
    {
        /// <summary>
        /// 执行任务ing
        /// </summary>
        /// <param name="context"></param>
        public override void OnExecuting(TaskContext context)
        {
            context.InstanceRun();
        }

        /// <summary>
        /// 执行完成
        /// </summary>
        /// <param name="context"></param>
        public override void OnExecuted(TaskContext context)
        {

        }
    }
}
