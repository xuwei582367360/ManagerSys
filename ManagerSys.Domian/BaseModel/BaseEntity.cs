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
        /// <summary>
        /// 创建人编码
        /// </summary>
        [MaxLength(50)]
        [Column("CreateUserCode")]
        public string? CreateUserCode { get; set; }  
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        [Column("CreateUser")]
        public string? CreateUser { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>
        [MaxLength(50)]
        [Column("CreateTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人编码
        /// </summary>
        [MaxLength(50)]
        [Column("UpdateUserCode")]
        public string? UpdateUserCode { get; set; } 
        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(50)]
        [Column("UpdateUser")]
        public string? UpdateUser { get; set; } 
        /// <summary>
        /// 修改时间
        /// </summary>
        [MaxLength(50)] 
        [Column("UpdateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
