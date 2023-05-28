﻿// <auto-generated />
using System;
using ManagerSys.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    [DbContext(typeof(BusDbContext))]
    partial class BusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.SqlServer)
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ManagerSys.Domian.Business.SysLog", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasComment("自增Id");

                    b.Property<DateTime?>("CreateTime")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateTime")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUser")
                        .HasComment("创建人");

                    b.Property<string>("CreateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUserCode")
                        .HasComment("创建人编码");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted")
                        .HasComment("是否删除");

                    b.Property<int>("LogType")
                        .HasColumnType("int")
                        .HasColumnName("LogType")
                        .HasComment("日志类型 1、 操作日志  2、错误日志");

                    b.Property<string>("MacAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("MacAddress")
                        .HasComment("操作人MAC地址");

                    b.Property<string>("OperateContent")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OperateContent")
                        .HasComment("操作内容描述");

                    b.Property<string>("OperateType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("OperateType")
                        .HasComment(" 操作类型   添加  修改 删除 查询");

                    b.Property<string>("OperateUserIp")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("OperateUserIp")
                        .HasComment("操作人IP");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Page")
                        .HasComment("所属页面");

                    b.Property<DateTime?>("UpdateTime")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateTime")
                        .HasComment("修改时间");

                    b.Property<string>("UpdateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUser")
                        .HasComment("修改人");

                    b.Property<string>("UpdateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUserCode")
                        .HasComment("修改人编码");

                    b.HasKey("Id");

                    b.ToTable("Sys_Log");
                });

            modelBuilder.Entity("ManagerSys.Domian.Business.SystemUserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreateTime")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateTime")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUser")
                        .HasComment("创建人");

                    b.Property<string>("CreateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUserCode")
                        .HasComment("创建人编码");

                    b.Property<string>("Email")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted")
                        .HasComment("是否删除");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastLoginTime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Phone");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("RealName");

                    b.Property<int?>("Status")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdateTime")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateTime")
                        .HasComment("修改时间");

                    b.Property<string>("UpdateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUser")
                        .HasComment("修改人");

                    b.Property<string>("UpdateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUserCode")
                        .HasComment("修改人编码");

                    b.Property<string>("UserCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UserCode");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("Sys_Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateTime = new DateTime(2023, 5, 26, 21, 28, 25, 609, DateTimeKind.Local).AddTicks(4530),
                            CreateUser = "",
                            CreateUserCode = "",
                            Email = "15086691491@qq.com",
                            IsDeleted = false,
                            Password = "96e79218965eb72c92a549dd5a330112",
                            Phone = "15086691491",
                            RealName = "admin",
                            Status = 1,
                            UpdateUser = "",
                            UpdateUserCode = "",
                            UserCode = "001",
                            UserName = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
