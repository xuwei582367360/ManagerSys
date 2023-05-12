using ManagerSys.Domian.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ManagerSys.Domian.Business
{
    [Table("SystemUsers")]
    public class SystemUserEntity : BaseEntity<int>
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Column("UserCode")]
        public string? UserCode { get; set; }

        [Required, MaxLength(50)]
        [Column("Username")]
        public string? UserName { get; set; }

        [Required, MaxLength(50)]
        [Column("Password")]
        public string? Password { get; set; }

        [Required, MaxLength(50)]
        [Column("RealName")]
        public string? RealName { get; set; }

        [MaxLength(15)]
        [Column("Phone")]
        public string? Phone { get; set; }

        [MaxLength(500), EmailAddress(ErrorMessage = "邮箱格式错误")]
        [Column("Email")]
        public string? Email { get; set; }

        [Required]
        [Column("Status")]
        public int? Status { get; set; }

        [Column("LastLoginTime")]
        public DateTime? LastLoginTime { get; set; }
    }

    public enum SystemUserStatus
    {
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = -1,

        /// <summary>
        /// 已锁定
        /// </summary>
        [Description("已锁定")]
        Disabled = 0,

        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Available = 1
    }
}
