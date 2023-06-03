using ManagerSys.Domian.Schedule;
using Microsoft.EntityFrameworkCore;
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
           
            //字段类型适配
            builder.FixColumnsDataType<ScheduleDelayedEntity>();
            builder.FixColumnsDataType<ScheduleEntity>();
            builder.FixColumnsDataType<ScheduleExecutorEntity>();
            builder.FixColumnsDataType<ScheduleHttpOptionEntity>();
            builder.FixColumnsDataType<ScheduleKeeperEntity>();
            builder.FixColumnsDataType<ScheduleLockEntity>();
            builder.FixColumnsDataType<ScheduleReferenceEntity>();
            builder.FixColumnsDataType<ScheduleTraceEntity>();
            builder.FixColumnsDataType<ServerNodeEntity>();
            builder.FixColumnsDataType<SystemConfigEntity>();
            builder.FixColumnsDataType<TraceStatisticsEntity>();
            //初始化种子数据
            ModelBuilderExtensions.SeedScheData(builder);
            //builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
