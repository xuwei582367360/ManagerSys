using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian
{
    public static class ManagerSysConsts
    {
        public const string Provider = "Provider";

        public const string DeSchema = null;
        public const string BusSchema = null;
    }

    public enum AbpBaseDataType
    {
        Orcal = 0,
        Mysql = 1,
        Sqlserver = 2
        // 其他数据库
    }


}
