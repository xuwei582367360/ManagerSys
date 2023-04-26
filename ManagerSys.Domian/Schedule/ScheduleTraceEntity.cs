using ManagerSys.Domian.BaseModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleTraces")]
    public class ScheduleTraceEntity : BaseEntity<Guid>
    {
        [Key]
        [Column("TraceId")]
        public Guid TraceId { get; set; }

        [Column("ScheduleId")]
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// 所在节点
        /// </summary>
        [Column("Node")]
        [MaxLength(100)]
        public string Node { get; set; }

        /// <summary>
        /// 开始运行时间
        /// </summary>
        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束运行时间
        /// </summary>
        [Column("EndTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 执行耗时，单位是秒
        /// </summary>
        [Column("ElapsedTime")]
        public double ElapsedTime { get; set; }

        /// <summary>
        /// 运行结果 0-无结果 1-运行成功 2-运行失败 3- 互斥取消 
        /// </summary>
        [Column("Result")]
        public int Result { get; set; }

    }

    /// <summary>
    /// 任务运行结果
    /// </summary>
    public enum ScheduleRunResult
    {
        /// <summary>
        /// 无结果
        /// </summary>
        [Description("无结果")]
        Null = 0,

        /// <summary>
        /// 运行成功
        /// </summary>
        [Description("运行成功")]
        Success = 1,

        /// <summary>
        /// 运行失败
        /// </summary>
        [Description("运行失败")]
        Failed = 2,

        /// <summary>
        /// 互斥取消
        /// </summary>
        [Description("互斥取消")]
        Conflict = 3

    }
}
