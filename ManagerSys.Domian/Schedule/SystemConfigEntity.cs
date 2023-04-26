using ManagerSys.Domian.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerSys.Domian.Schedule
{
    [Table("SystemConfigs")]
    public class SystemConfigEntity : BaseEntity<string>
    {
        [Key, MaxLength(50)]
        [Column("key")]
        public string Key { get; set; }

        [Required, MaxLength(50)]
        [Column("Group")]
        public string Group { get; set; }

        [Required, MaxLength(100)]
        [Column("Name")]
        public string Name { get; set; }

        [MaxLength(1000)]
        [Column("Value")]
        public string Value { get; set; }

        [Column("Sort")]
        public int Sort { get; set; }

        [Column("IsReuired")]
        public bool IsReuired { get; set; }

        [MaxLength(500)]
        [Column("Remark")]
        public string Remark { get; set; }
    }
}
