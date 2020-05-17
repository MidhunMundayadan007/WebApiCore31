using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTraining.DAL.Migrations
{
    public partial class loggingdetailsq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "UserRegistrations");

            migrationBuilder.DropColumn(
                name: "TokenExpireDateandTime",
                table: "UserLoggedInDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "UserRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenExpireDateandTime",
                table: "UserLoggedInDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
