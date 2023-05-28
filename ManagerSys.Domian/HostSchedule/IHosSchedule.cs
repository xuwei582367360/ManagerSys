using ManagerSys.Domain.Shared.QuartzNet.Base;
using ManagerSys.Domian.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian.HostSchedule
{
    public interface IHosSchedule
    {
        ScheduleEntity Schedule { get; set; }

        //Dictionary<string, object> CustomParams { get; set; }

        //List<KeyValuePair<string, string>> Keepers { get; set; }

        //Dictionary<Guid, string> Children { get; set; }

        ScheBaseEntity RunnableInstance { get; set; }

        CancellationTokenSource CancellationTokenSource { get; set; }

        void CreateRunnableInstance(ScheduleOperation context);

        Type GetQuartzJobType();

        void Dispose();
    }

}
