using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("TraceStatistics")]
    public class TraceStatisticsEntity :  BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DateNum")]
        public int DateNum { get; set; }


        [Column("DateStamp")]
        public long DateStamp { get; set; }

        [Column("Success")]
        public int Success { get; set; }

        [Column("Fail")]
        public int Fail { get; set; }

        [Column("Other")]
        public int Other { get; set; }
    }
}
