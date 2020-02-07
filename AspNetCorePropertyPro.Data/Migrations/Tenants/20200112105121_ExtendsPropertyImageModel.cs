using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCorePropertyPro.Data.Migrations.Tenants
{
    public partial class ExtendsPropertyImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "PropertyImages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "PropertyImages");
        }
    }
}
