using ManagerSys.Domian.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.EntityFrameworkCore
{
    public class EntityFrameworkCoreManagerSysDbSchemaMigrator
            : IManagerSysDbSchemaMigrator
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreManagerSysDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the BookStoreDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<CDZLSDbContext>()
                .Database
                .MigrateAsync();

            await _serviceProvider
                .GetRequiredService<CDZLSTestDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
