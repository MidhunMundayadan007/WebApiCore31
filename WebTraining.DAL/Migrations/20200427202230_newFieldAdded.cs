using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTraining.DAL.Migrations
{
    public partial class newFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOfCreated",
                table: "Topics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "Topics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Topics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreated",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Topics");
        }
    }
}
