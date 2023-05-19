using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    public partial class init003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 16, 16, 40, 31, 576, DateTimeKind.Local).AddTicks(2627));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 14, 17, 44, 23, 978, DateTimeKind.Local).AddTicks(1203));
        }
    }
}
