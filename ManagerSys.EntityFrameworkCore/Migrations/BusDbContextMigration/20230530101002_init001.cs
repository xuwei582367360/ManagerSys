﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    public partial class init001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "自增Id"),
                    LogType = table.Column<int>(type: "int", nullable: false, comment: "日志类型 1、 操作日志  2、错误日志"),
                    Page = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "所属页面"),
                    OperateType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: " 操作类型   添加  修改 删除 查询"),
                    OperateContent = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true, comment: "操作内容描述"),
                    OperateUserIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "操作人IP"),
                    MacAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "操作人MAC地址"),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建人编码"),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true, comment: "创建时间"),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人编码"),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人"),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键ID"),
                    MenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "菜单名称"),
                    MenuCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "菜单编码，用于后端权限控制"),
                    ParentMenuGuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "菜单父节点ID，方便递归遍历菜单"),
                    NodeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "节点名称：1菜单，2页面，3按钮"),
                    IconUrl = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "图标URL"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    LinkUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "页面对应的地址，如果是文件夹或者按钮类型，可以为空"),
                    Level = table.Column<int>(type: "int", nullable: false, comment: "菜单树的层次，以便于查询指定层级的菜单"),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "菜单路径"),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建人编码"),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true, comment: "创建时间"),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人编码"),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人"),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RealName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建人编码"),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建人"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true, comment: "创建时间"),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人编码"),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人"),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Sys_Users",
                columns: new[] { "Id", "CreateTime", "CreateUser", "CreateUserCode", "Email", "LastLoginTime", "Password", "Phone", "RealName", "Status", "UpdateTime", "UpdateUser", "UpdateUserCode", "UserCode", "Username" },
                values: new object[] { 1, new DateTime(2023, 5, 30, 18, 10, 1, 844, DateTimeKind.Local).AddTicks(7673), "", "", "15086691491@qq.com", null, "96e79218965eb72c92a549dd5a330112", "15086691491", "admin", 1, null, "", "", "001", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Log");

            migrationBuilder.DropTable(
                name: "Sys_Menu");

            migrationBuilder.DropTable(
                name: "Sys_Users");
        }
    }
}
