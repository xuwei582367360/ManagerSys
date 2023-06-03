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
        删除,
        启动服务,
        停止服务,
        暂停服务,
        恢复运行,
        立即运行一次
    }
}
