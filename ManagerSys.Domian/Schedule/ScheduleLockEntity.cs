using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleLocks")]
    public class ScheduleLockEntity : BaseEntity<Guid>
    {
        /// <summary>
        /// 调度编号
        /// </summary>
        [Key]
        [Column("ScheduleId")]
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        [Column("Status")]
        public int Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Column("LockedTime")]
        public DateTime? LockedTime { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        [Column("LockedNode")]
        [MaxLength(100)]
        public string LockedNode { get; set; }
    }

}
