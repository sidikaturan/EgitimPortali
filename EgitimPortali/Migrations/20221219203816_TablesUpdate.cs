using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgitimPortali.Migrations
{
    public partial class TablesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoruSirasi",
                table: "TestSorus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Dogruluk",
                table: "SorularinCevaplaris",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KonuSirasi",
                table: "Konulars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoruSirasi",
                table: "TestSorus");

            migrationBuilder.DropColumn(
                name: "Dogruluk",
                table: "SorularinCevaplaris");

            migrationBuilder.DropColumn(
                name: "KonuSirasi",
                table: "Konulars");
        }
    }
}
