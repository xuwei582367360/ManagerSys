using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.BusDbContextMigration
{
    public partial class init002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 26, 21, 28, 25, 609, DateTimeKind.Local).AddTicks(4530));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sys_Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateTime",
                value: new DateTime(2023, 5, 26, 21, 27, 3, 639, DateTimeKind.Local).AddTicks(2092));
        }
    }
}
