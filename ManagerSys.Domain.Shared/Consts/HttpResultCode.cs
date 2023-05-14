using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.Consts
{
    /// <summary>
    /// 状态码管理
    /// </summary>
    public static class HttpResultCode
    {
        /// <summary>
        /// 成功状态
        /// </summary>
        public const int Success = 200;
        /// <summary>
        /// 数据验证不合法
        /// </summary>
        public const int Fail = 412;

        /// <summary>
        /// 无权限
        /// </summary>
        public const int NoPermission = 401;
        /// <summary>
        /// 后台错误
        /// </summary>
        public const int Error = 500;

        /// <summary>
        /// 找不到服务器
        /// </summary>
        public const int NoPath = 404;
        /// <summary>
        /// 第三方错误
        /// </summary>
        public const int TripartiteError = 302500;
    }
}
