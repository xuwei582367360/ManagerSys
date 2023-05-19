﻿// <auto-generated />
using System;
using ManagerSys.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    [DbContext(typeof(BusDbContext))]
    [Migration("20230516093422_init004")]
    partial class init004
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreateTime")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateTime");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUser");

                    b.Property<string>("CreateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUserCode");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<int>("LogType")
                        .HasColumnType("int")
                        .HasColumnName("LogType");

                    b.Property<string>("MacAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("MacAddress");

                    b.Property<string>("OperateContent")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OperateContent");

                    b.Property<string>("OperateType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("OperateType");

                    b.Property<string>("OperateUserIp")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("OperateUserIp");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Page");

                    b.Property<DateTime?>("UpdateTime")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateTime");

                    b.Property<string>("UpdateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUser");

                    b.Property<string>("UpdateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUserCode");

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
                        .HasColumnName("CreateTime");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUser");

                    b.Property<string>("CreateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CreateUserCode");

                    b.Property<string>("Email")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

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
                        .HasColumnName("UpdateTime");

                    b.Property<string>("UpdateUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUser");

                    b.Property<string>("UpdateUserCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("UpdateUserCode");

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
                            CreateTime = new DateTime(2023, 5, 16, 17, 34, 22, 682, DateTimeKind.Local).AddTicks(9479),
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
