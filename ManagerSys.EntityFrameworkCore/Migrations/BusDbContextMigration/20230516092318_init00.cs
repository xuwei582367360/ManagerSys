using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    public partial class init00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperateContent",
                table: "Sys_Log",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 16, 17, 23, 18, 302, DateTimeKind.Local).AddTicks(579));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperateContent",
                table: "Sys_Log",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647);

            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 16, 16, 40, 31, 576, DateTimeKind.Local).AddTicks(2627));
        }
    }
}
