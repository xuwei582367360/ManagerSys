using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace ManagerSys.Domian.BaseModel
{
    public class BaseEntity<T> :Entity<T>, ISoftDelete
    {
        [MaxLength(50)]
        [Column("CreateUserCode")]
        public string CreateUserCode { get; set; }

        [MaxLength(50)]
        [Column("CreateUser")]
        public string CreateUser { get; set; }

        [MaxLength(50)]
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

        [MaxLength(50)]
        [Column("UpdateUserCode")]
        public string UpdateUserCode { get; set; }

        [MaxLength(50)]
        [Column("UpdateUser")]
        public string UpdateUser { get; set; }

        [MaxLength(50)]
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
