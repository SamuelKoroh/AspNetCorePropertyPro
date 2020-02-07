using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCorePropertyPro.Data.Migrations.Tenants
{
    public partial class ExtendsPropertyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Properties",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Properties",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);
        }
    }
}
