using ManagerSys.Application.Contracts.Menu;
using ManagerSys.Application.Contracts.Menu.Dto;
using ManagerSys.Domain.Shared.BaseAttribute;
using ManagerSys.Domain.Shared.Enums;
using ManagerSys.Domain.Shared.PageModel;
using ManagerSys.Domian.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.HttpApi.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
 
        /// <summary>
        ///递归查询菜单树
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getTreeList"), Explain("【菜单】", OperateType.查询)]
        public async Task<BasePage<List<MenuListDto>>> GetTreeList()
        {
            return await _menuService.GetTreeList();
        }


        /// <summary>
        ///新增菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("add"), Explain("【菜单】", OperateType.添加)]
        public async Task<BasePage<SysMenu>> Add(MenuAddDto menuAddDto)
        {
            return await _menuService.Add(menuAddDto);
        }

        /// <summary>
        ///修改菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("update"), Explain("【菜单】", OperateType.修改)]
        public async Task<BasePage<SysMenu>> Update(MenuAddDto menuAddDto)
        {
            return await _menuService.Update(menuAddDto);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuAddDto"></param>
        /// <returns></returns>
        [HttpPost, Route("delete"), Explain("【菜单】", OperateType.删除)]
        public async Task<BasePage<bool>> Delete(Guid menuGuid)
        {
            return await _menuService.Delete(menuGuid);
        }
    }
}
