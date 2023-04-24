using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian.Data
{
    public interface IManagerSysDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
