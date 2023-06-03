using ManagerSys.Application.Contracts.Menu.Dto;
using ManagerSys.Domain.Shared.PageModel;
using ManagerSys.Domian.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Menu
{
    public interface IMenuService
    {

        /// <summary>
        /// 查询菜单树结构
        /// </summary>
        /// <returns></returns>
        Task<BasePage<List<MenuListDto>>> GetTreeList();
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        /// <returns></returns>
        Task<BasePage<SysMenu>> Add(MenuAddDto sysMenu);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuAddDto"></param>
        /// <returns></returns>
        Task<BasePage<SysMenu>> Update(MenuAddDto menuAddDto);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuGuid"></param>
        /// <returns></returns>
        Task<BasePage<bool>> Delete(Guid menuGuid);
    }
}
