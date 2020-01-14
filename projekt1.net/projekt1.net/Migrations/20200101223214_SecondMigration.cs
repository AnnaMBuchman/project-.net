using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt1.net.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobOfers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobOfers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "JobOfers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JobOfers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobOfers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryFrom",
                table: "JobOfers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryTo",
                table: "JobOfers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "JobOfers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOfers_CompanyId",
                table: "JobOfers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfers_Companies_CompanyId",
                table: "JobOfers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfers_Companies_CompanyId",
                table: "JobOfers");

            migrationBuilder.DropIndex(
                name: "IX_JobOfers_CompanyId",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "SalaryFrom",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "SalaryTo",
                table: "JobOfers");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "JobOfers");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobOfers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
