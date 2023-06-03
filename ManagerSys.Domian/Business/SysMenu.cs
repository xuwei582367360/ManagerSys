using ManagerSys.Domian.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian.Business
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("Sys_Menu")]
    public class SysMenu : BaseEntity<Guid>
    {
        public SysMenu()
        {
            this.Id = Guid.NewGuid();
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("Id")]
        [Description("主键ID")]
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(100)]
        [Column("MenuName"), Required]
        [Description("菜单名称")]
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary>
        [MaxLength(50)]
        [Column("MenuCode"), Required]
        [Description("菜单编码，用于后端权限控制")]
        public string MenuCode { get; set; }
        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [MaxLength(50)]
        [Column("ParentMenuGuid"), Required]
        [Description("菜单父节点ID，方便递归遍历菜单")]
        public string ParentMenuGuid { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        [MaxLength(100)]
        [Column("NodeType"), Required]
        [Description("节点名称：1菜单，2页面，3按钮")]
        public string NodeType { get; set; }
        /// <summary>
        /// 图标URL
        /// </summary>
        [MaxLength(20)]
        [Column("IconUrl")]
        [Description("图标URL")]
        public string IconUrl { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Column("Sort")]
        [Description("序号")]
        public int Sort { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        [MaxLength(100)]
        [Column("LinkUrl")]
        [Description("页面对应的地址，如果是文件夹或者按钮类型，可以为空")]
        public string LinkUrl { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [Column("Level"), Required]
        [Description("菜单树的层次，以便于查询指定层级的菜单")]
        public int Level { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        [MaxLength(100)]
        [Column("Path")]
        [Description("菜单路径")]
        public string Path { get; set; }
    }
}
