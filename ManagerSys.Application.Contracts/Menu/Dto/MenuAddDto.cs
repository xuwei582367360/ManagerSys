using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Menu.Dto
{
    public class MenuAddDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode { get; set; }
        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public string ParentMenuGuid { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeType { get; set; }
        /// <summary>
        /// 图标URL
        /// </summary>
        public string IconUrl { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { get; set; }
    }
}
