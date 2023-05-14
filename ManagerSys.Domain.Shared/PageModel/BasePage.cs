using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.PageModel
{
    /// <summary>
    /// 返回信息基类
    /// </summary>
    public class BasePage
    {

        /// <summary>
        /// 状态码200
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 显示信息
        /// </summary>
        public string Message { get; set; }
    }
}
