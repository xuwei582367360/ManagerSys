using ManagerSys.Domain.Shared.Common;
using ManagerSys.Domian.Business;
using ManagerSys.Domian.Schedule;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace ManagerSys.EntityFrameworkCore
{
    [ConnectionStringName("Business")]
    public class BusDbContext : AbpDbContext<BusDbContext>
    {

        public DbSet<SystemUserEntity> systemUserEntities { get; set; }

        public BusDbContext(DbContextOptions<BusDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //字段类型适配
            builder.FixColumnsDataType<SystemUserEntity>();
            //初始化种子数据
            ModelBuilderExtensions.SeedBusData(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
