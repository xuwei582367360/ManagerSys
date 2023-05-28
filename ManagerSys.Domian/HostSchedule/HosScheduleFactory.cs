using ManagerSys.Domian.Schedule;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian.HostSchedule
{
    public class HosScheduleFactory
    {
        public static async Task<IHosSchedule> GetHosSchedule(ScheduleOperation context)
        {
            IHosSchedule result = null;
            switch ((ScheduleMetaType)context.Schedule.MetaType)
            {
                case ScheduleMetaType.Assembly:
                    {
                        //result = new AssemblySchedule();
                        //await LoadPluginFile(context.Schedule);
                        break;
                    }
                case ScheduleMetaType.Http:
                    {
                        result = new HttpSchedule();
                        break;
                    }
                default: throw new InvalidOperationException("unknown schedule type.");
            }
            result.Schedule = context.Schedule;
            //result.CustomParams = ConvertParamsJson(context.Schedule.CustomParamsJson);
            //result.Keepers = context.Keepers;
            //result.Children = context.Children;
            result.CancellationTokenSource = new System.Threading.CancellationTokenSource();
            result.CreateRunnableInstance(context);
            result.RunnableInstance.TaskId = context.Schedule.Id;
            result.RunnableInstance.CancellationToken = result.CancellationTokenSource.Token;
            result.RunnableInstance.Initialize();
            return result;
        }

        public static Dictionary<string, object> ConvertParamsJson(string source)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                List<ScheduleParam> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ScheduleParam>>(source);
                foreach (var item in list)
                {
                    result[item.ParamKey] = item.ParamValue;
                }
            }
            catch (Exception ex)
            {
               
            }
            return result;
        }
    }

}
