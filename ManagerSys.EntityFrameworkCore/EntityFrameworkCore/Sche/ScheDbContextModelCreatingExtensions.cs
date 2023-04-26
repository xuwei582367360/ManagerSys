using ManagerSys.Domian;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ManagerSys.EntityFrameworkCore.EntityFrameworkCore
{
    public static class ScheDbContextModelCreatingExtensions
    {
        public static void ConfigureBookStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            //builder.Entity<UserInfo>(b =>
            //{
            //    b.ToTable(ManagerSysConsts.DbTablePrefix + "Books", ManagerSysConsts.DbSchema);
            //    b.ConfigureByConvention();
            //    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            //});
        }
    }
}
