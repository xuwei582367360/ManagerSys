using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("ScheduleReferences")]
    public class ScheduleReferenceEntity : BaseEntity<int>
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("ScheduleId")]
        public Guid ScheduleId { get; set; }

        [Column("ChildId")]
        public Guid ChildId { get; set; }
    }
}
