using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgitimPortali.Migrations
{
    public partial class TestSoruGorselAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GorselA",
                table: "TestSorus",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GorselB",
                table: "TestSorus",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GorselC",
                table: "TestSorus",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GorselD",
                table: "TestSorus",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GorselE",
                table: "TestSorus",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SoruGorsel",
                table: "TestSorus",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GorselA",
                table: "TestSorus");

            migrationBuilder.DropColumn(
                name: "GorselB",
                table: "TestSorus");

            migrationBuilder.DropColumn(
                name: "GorselC",
                table: "TestSorus");

            migrationBuilder.DropColumn(
                name: "GorselD",
                table: "TestSorus");

            migrationBuilder.DropColumn(
                name: "GorselE",
                table: "TestSorus");

            migrationBuilder.DropColumn(
                name: "SoruGorsel",
                table: "TestSorus");
        }
    }
}
