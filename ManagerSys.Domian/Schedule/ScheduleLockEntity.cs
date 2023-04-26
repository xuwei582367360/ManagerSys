using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleLocks")]
    public class ScheduleLockEntity : BaseEntity<Guid>
    {
        [Key]
        [Column("ScheduleId")]
        public Guid ScheduleId { get; set; }

        [Required]
        [Column("Status")]
        public int Status { get; set; }

        [Column("LockedTime")]
        public DateTime? LockedTime { get; set; }

        [Column("LockedNode")]
        [MaxLength(100)]
        public string LockedNode { get; set; }
    }

}
