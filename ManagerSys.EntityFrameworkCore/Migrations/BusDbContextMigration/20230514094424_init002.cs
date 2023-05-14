using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    public partial class init002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemUsers",
                table: "SystemUsers");

            migrationBuilder.RenameTable(
                name: "SystemUsers",
                newName: "Sys_Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sys_Users",
                table: "Sys_Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sys_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    Page = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperateType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperateContent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OperateUserIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MacAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Log", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 14, 17, 44, 23, 978, DateTimeKind.Local).AddTicks(1203));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sys_Users",
                table: "Sys_Users");

            migrationBuilder.RenameTable(
                name: "Sys_Users",
                newName: "SystemUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemUsers",
                table: "SystemUsers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 11, 16, 10, 10, 130, DateTimeKind.Local).AddTicks(1082));
        }
    }
}
