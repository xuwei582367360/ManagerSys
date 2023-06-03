using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Application.Contracts.Menu.Dto
{
    public class MenuListDto: MenuAddDto
    {
        public List<MenuListDto> Childrens { get; set; }
    }
}
