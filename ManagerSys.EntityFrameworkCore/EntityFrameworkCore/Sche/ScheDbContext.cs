using ManagerSys.Domian.CDZLS;
using ManagerSys.Domian.Schedule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ManagerSys.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class ScheDbContext : AbpDbContext<ScheDbContext>
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        //public DbSet<AreaEntity> Users { get; set; }

        public DbSet<ScheduleDelayedEntity> scheduleDelayedEntities { get; set; }
        public DbSet<ScheduleEntity> scheduleEntities { get; set; }
        public DbSet<ScheduleExecutorEntity> scheduleExecutorEntities { get; set; }
        public DbSet<ScheduleHttpOptionEntity> scheduleHttpOptionEntities { get; set; }
        public DbSet<ScheduleKeeperEntity> scheduleKeeperEntities { get; set; }
        public DbSet<ScheduleLockEntity> scheduleLockEntities { get; set; }
        public DbSet<ScheduleReferenceEntity> scheduleReferenceEntities { get; set; }
        public DbSet<ScheduleTraceEntity> scheduleTraceEntities { get; set; }
        public DbSet<ServerNodeEntity> serverNodeEntities { get; set; }
        public DbSet<SystemConfigEntity> systemConfigEntities { get; set; }
        public DbSet<TraceStatisticsEntity> traceStatisticsEntities { get; set; }
        #endregion
        public ScheDbContext(DbContextOptions<ScheDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            //builder.ConfigureBackgroundJobs();
            //builder.ConfigureIdentity();
            //builder.ConfigureIdentityServer();
            //builder.ConfigureFeatureManagement();
            //builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }

    }
}
