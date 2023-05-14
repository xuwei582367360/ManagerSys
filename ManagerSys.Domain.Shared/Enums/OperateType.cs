using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.Enums
{
    [Description("操作类型")]
    public enum OperateType
    {
        查询,
        添加,
        修改,
        删除
    }
}
