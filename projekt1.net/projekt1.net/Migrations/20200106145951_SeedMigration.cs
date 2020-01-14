using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt1.net.Migrations
{
    public partial class SeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobOfers",
                columns: new[] { "Id", "CompanyId", "Created", "Description", "JobTitle", "Location", "SalaryFrom", "SalaryTo", "ValidUntil" },
                values: new object[] { 1, 1, new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "You can teach math in high school", "Teacher", "Warsaw", 1000m, 10000m, new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "JobOfers",
                columns: new[] { "Id", "CompanyId", "Created", "Description", "JobTitle", "Location", "SalaryFrom", "SalaryTo", "ValidUntil" },
                values: new object[] { 2, 2, new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "You will be able to lead the project", "Manager", "Warsaw", 10000m, 20000m, new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobOfers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOfers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
