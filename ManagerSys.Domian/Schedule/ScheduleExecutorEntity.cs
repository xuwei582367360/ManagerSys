using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleExecutors")]
    public class ScheduleExecutorEntity : BaseEntity<int>
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        [Column("ScheduleId")]
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// worker名称
        /// </summary>
        [Column("WorkerName")]
        [MaxLength(100)]
        public string WorkerName { get; set; }
    }
}
