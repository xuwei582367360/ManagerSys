﻿using ManagerSys.Domian.BaseModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("Schedules")]
    public class ScheduleEntity : BaseEntity<Guid>
    {
        public ScheduleEntity()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// 任务id
        /// </summary>
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Required, MaxLength(100)]
        [Column("Title")]
        public string Title { get; set; }

        /// <summary>
        /// 任务类型
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
        /// 任务所在程序集
        /// </summary>
        [MaxLength(200)]
        [Column("AssemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// 任务的类型
        /// </summary>
        [MaxLength(200)]
        [Column("ClassName")]
        public string ClassName { get; set; }

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


    }


    /// <summary>
    /// 任务状态
    /// </summary>
    public enum ScheduleStatus
    {
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = -1,

        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stop = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running = 1,

        /// <summary>
        /// 已暂停
        /// </summary>
        [Description("已暂停")]
        Paused = 2

    }

    /// <summary>
    /// 任务类型
    /// </summary>
    public enum ScheduleMetaType
    {
        /// <summary>
        /// 程序集任务
        /// </summary>
        [Description("程序集任务")]
        Assembly = 1,

        /// <summary>
        /// HTTP任务
        /// </summary>
        [Description("HTTP任务")]
        Http = 2
    }
}
