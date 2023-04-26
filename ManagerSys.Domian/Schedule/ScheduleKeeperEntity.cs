using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleKeepers")]
    public class ScheduleKeeperEntity : BaseEntity<int>
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("ScheduleId")]
        public Guid ScheduleId { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }
    }
}
