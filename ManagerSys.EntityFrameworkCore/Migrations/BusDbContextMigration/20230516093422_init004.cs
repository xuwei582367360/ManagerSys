using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    public partial class init004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 16, 17, 34, 22, 682, DateTimeKind.Local).AddTicks(9479));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 16, 17, 23, 18, 302, DateTimeKind.Local).AddTicks(579));
        }
    }
}
