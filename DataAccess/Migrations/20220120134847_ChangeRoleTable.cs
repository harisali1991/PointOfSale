using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChangeRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "tblRoles");

            migrationBuilder.AddColumn<string>(
                name: "Descripton",
                table: "tblRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripton",
                table: "tblRoles");

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "tblRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
