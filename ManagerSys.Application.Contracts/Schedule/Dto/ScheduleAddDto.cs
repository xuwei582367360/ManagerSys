using ManagerSys.Domian.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Schedule.Dto
{
    public class ScheduleAddDto
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required, MaxLength(100)]
        [Column("Title")]
        public string Title { get; set; }

        /// <summary>
        /// 任务类型 Assembly,http
        /// </summary>
        [Required]
        [Column("MetaType")]
        public int MetaType { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        [MaxLength(500)]
        [Column("Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否周期运行
        /// </summary>
        [Required]
        [Column("RunLoop")]
        public bool RunLoop { get; set; }

        /// <summary>
        /// cron表达式
        /// </summary>
        [MaxLength(50)]
        [Column("CronExpression")]
        public string CronExpression { get; set; }

        /// <summary>
        /// 任务群组
        /// </summary>
        [MaxLength(500)]
        [Column("GroupName")]
        public string GroupName { get; set; }
        /// <summary>
        /// 任务所在程序集
        /// </summary>
        [MaxLength(200)]
        [Column("AssemblyName")]
        public string AssemblyName { get; set; }

        ///// <summary>
        ///// 任务的类型
        ///// </summary>
        //[MaxLength(200)]
        //[Column("ClassName")]
        //public string ClassName { get; set; }

        /// <summary>
        /// 自定义参数（json格式）
        /// </summary>
        [Column("CustomParamsJson")]
        public string CustomParamsJson { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [Required]
        [Column("Status")]
        public int Status { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [Column("StartDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [Column("EndDate")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        [Column("LastRunTime")]
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        [Column("NextRunTime")]
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 总运行成功次数
        /// </summary>
        [Column("TotalRunCount")]
        public int TotalRunCount { get; set; }


        /// <summary>
        /// 请求地址
        /// </summary>
        [Column("RequestUrl"), MaxLength(500), Required]
        [MapperSetting(BindField = "HttpRequestUrl")]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [Column("Method"), MaxLength(10), Required]
        [MapperSetting(BindField = "HttpMethod")]
        public string Method { get; set; }

        /// <summary>
        /// 数据格式
        /// </summary>
        [Column("ContentType"), MaxLength(50), Required]
        [MapperSetting(BindField = "HttpContentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// 自定义请求头（json格式）
        /// </summary>
        [Column("Headers")]
        [MapperSetting(BindField = "HttpHeaders")]
        public string Headers { get; set; }

        /// <summary>
        /// 数据内容（json格式）
        /// </summary>
        [Column("Body")]
        [MapperSetting(BindField = "HttpBody")]
        public string Body { get; set; }
    }
}
