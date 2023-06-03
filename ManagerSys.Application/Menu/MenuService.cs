using ManagerSys.Application.Contracts.Menu;
using ManagerSys.Application.Contracts.Menu.Dto;
using ManagerSys.Domain.Shared.PageModel;
using ManagerSys.Domian.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ManagerSys.Application.Menu
{
    public class MenuService : ApplicationAppService, IMenuService
    {
        private readonly IRepository<SysMenu, Guid> _sysMenuRepository;
        public MenuService(IRepository<SysMenu, Guid> sysMenuRepository)
        {
            _sysMenuRepository = sysMenuRepository;
        }

        /// <summary>
        /// 查询菜单树结构
        /// </summary>
        /// <returns></returns>
        public async Task<BasePage<List<MenuListDto>>> GetTreeList()
        {
            var basePage = new BasePage<List<MenuListDto>>() { Code = 200, Message = "获取成功" };
            //递归查询
            basePage.Data = await GetMenuTree("0");
            return basePage;
        }

        /// <summary>
        /// 递归查询菜单树
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        public async Task<List<MenuListDto>> GetMenuTree(string parentGuid)
        {
            var list = new List<MenuListDto>();
            var parentTree = new List<SysMenu>();
            parentTree = await _sysMenuRepository.GetListAsync(x => x.ParentMenuGuid == parentGuid);

            if (parentTree != null && parentTree.Count > 0)
            {
                parentTree.ForEach(x =>
               {
                   var menuListDto = new MenuListDto
                   {
                       Id = x.Id,
                       MenuName = x.MenuName,
                       MenuCode = x.MenuCode,
                       ParentMenuGuid = parentGuid,
                       NodeType = x.NodeType,
                       IconUrl = x.IconUrl,
                       Sort = x.Sort,
                       LinkUrl = x.LinkUrl,
                       Level = x.Level,
                       Path = x.Path,
                       Childrens = GetMenuTree(x.Id.ToString()).Result
                   };
                   list.Add(menuListDto);
               });
            }
            return list;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        /// <returns></returns>
        public async Task<BasePage<SysMenu>> Add(MenuAddDto menuAddDto)
        {
            var sysMenu = ObjectMapper.Map<MenuAddDto, SysMenu>(menuAddDto);
            var basePage = new BasePage<SysMenu>() { Code = 200, Message = "新增成功" };
            var findEntity = await _sysMenuRepository.FindAsync(x => x.MenuCode == sysMenu.MenuCode);
            if (findEntity != null)
            {
                return new BasePage<SysMenu>() { Code = 500, Message = "新增失败，菜单编码不允许重复！" };
            }
            sysMenu.CreateTime = DateTime.Now;
            var data = await _sysMenuRepository.InsertAsync(sysMenu);
            basePage.Data = data;
            return basePage;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sysMenu"></param>
        /// <returns></returns>
        public async Task<BasePage<SysMenu>> Update(MenuAddDto menuAddDto)
        {
            var basePage = new BasePage<SysMenu>() { Code = 200, Message = "修改成功" };

            var sysMenu = ObjectMapper.Map<MenuAddDto, SysMenu>(menuAddDto);
            var findEntity = await _sysMenuRepository.AnyAsync(x => x.MenuCode == sysMenu.MenuCode);
            if (!findEntity)
            {
                return new BasePage<SysMenu>() { Code = 500, Message = "菜单不存在，请确认后再试！" };
            }
            sysMenu.UpdateTime = DateTime.Now;
            var data = await _sysMenuRepository.UpdateAsync(sysMenu);
            basePage.Data = data;
            return basePage;
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuGuid"></param>
        /// <returns></returns>
        public async Task<BasePage<bool>> Delete(Guid menuGuid)
        {
            var basePage = new BasePage<bool>() { Code = 200, Message = "删除成功", Data = true };
            await _sysMenuRepository.DeleteAsync(menuGuid);
            return basePage;
        }
    }
}
