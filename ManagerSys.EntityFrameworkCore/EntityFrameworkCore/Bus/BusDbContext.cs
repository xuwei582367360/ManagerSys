using ManagerSys.Domain.Shared.Common;
using ManagerSys.Domian.BaseModel;
using ManagerSys.Domian.Business;
using ManagerSys.Domian.Schedule;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace ManagerSys.EntityFrameworkCore
{
    [ConnectionStringName("Business")]
    public class BusDbContext : AbpDbContext<BusDbContext>
    {

        public DbSet<SystemUserEntity> systemUserEntities { get; set; }
        public DbSet<SysLog> sysLogs { get; set; }
        public DbSet<SysMenu> sysMenus { get; set; }
        public BusDbContext(DbContextOptions<BusDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //字段类型适配
            //builder.FixColumnsDataType<SystemUserEntity>();
            //初始化种子数据
            ModelBuilderExtensions.SeedBusData(builder);
            //builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            #region 字段注释
            var ddd = builder.Model.GetEntityTypes().ToList();
            var assembly = Assembly.LoadFrom(Directory.GetCurrentDirectory() + @"\bin\Debug\net6.0\ManagerSys.Domian.dll");
            foreach (var item in ddd)
            {
                var tabtype = assembly.GetType(item.ClrType.FullName);
                var props = tabtype.GetProperties();
                var descriptionAttrtable = tabtype.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (descriptionAttrtable.Length > 0)
                {
                    builder.Entity(item.Name).HasComment(((DescriptionAttribute)descriptionAttrtable[0]).Description);
                }
                foreach (var prop in props)
                {
                    var descriptionAttr = prop.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (descriptionAttr.Length > 0)
                    {
                        builder.Entity(item.Name).Property(prop.Name).HasComment(((DescriptionAttribute)descriptionAttr[0]).Description);
                    }
                }
            }
            #endregion
        }
    }
}
