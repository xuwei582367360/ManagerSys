using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("创建人编码")]
        public string? CreateUserCode { get; set; }  
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        [Column("CreateUser")]
        [Description("创建人")]
        public string? CreateUser { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>
        [MaxLength(50)]
        [Column("CreateTime")]
        [Description("创建时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人编码
        /// </summary>
        [MaxLength(50)]
        [Column("UpdateUserCode")]
        [Description("修改人编码")]
        public string? UpdateUserCode { get; set; } 
        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(50)]
        [Column("UpdateUser")]
        [Description("修改人")]
        public string? UpdateUser { get; set; } 
        /// <summary>
        /// 修改时间
        /// </summary>
        [MaxLength(50)] 
        [Column("UpdateTime")]
        [Description("修改时间")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("IsDeleted")]
        [Description("是否删除")]
        public bool IsDeleted { get; set; }
    }
}
