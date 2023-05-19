using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerSys.Domian.BaseModel;

namespace ManagerSys.Domian.Business
{
    /// <summary>
    /// 操作
    /// </summary>
    [Table("Sys_Log")]
    public class SysLog : BaseEntity<Guid>
    {
        public SysLog()
        {
            this.Id = Guid.NewGuid();
        }
        #region Entity
        /// <summary>
        /// 自增Id
        /// </summary>
        [Key]
        [Column("Id")]
        [Description("自增Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 日志类型 1、 操作日志  2、错误日志
        /// </summary>
        [Column("LogType")]
        [Description("日志类型 1、 操作日志  2、错误日志")]
        public int LogType { get; set; }

        /// <summary>
        /// 所属页面
        /// </summary>
        [MaxLength(50)]
        [Column("Page")]
        [Description("所属页面")]
        public string Page { get; set; }
        /// <summary>
        /// 操作类型   添加  修改 删除 查询
        /// </summary>
        [MaxLength(50)]
        [Column("OperateType")]
        [Description(" 操作类型   添加  修改 删除 查询")]
        public string OperateType { get; set; }

        /// <summary>
        /// 操作内容描述
        /// </summary>
        [MaxLength(int.MaxValue)]
        [Column("OperateContent")]
        [Description("操作内容描述")]
        public string OperateContent { get; set; }

        /// <summary>
        /// 操作人IP
        /// </summary>
        [MaxLength(50)]
        [Column("OperateUserIp")]
        [Description("操作人IP")]
        public string OperateUserIp { get; set; }

        /// <summary>
        /// 操作人MAC地址
        /// </summary>
        [MaxLength(100)]
        [Column("MacAddress")]
        [Description("操作人MAC地址")]
        public string MacAddress { get; set; }
        #endregion

       
    }
}
