using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTraining.DAL.Migrations
{
    public partial class loggingdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserRegistrations");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserRegistrations");

            migrationBuilder.CreateTable(
                name: "UserLoggedInDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    LoggedInDateandTime = table.Column<string>(nullable: true),
                    TokenExpireDateandTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoggedInDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLoggedInDetails");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserRegistrations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
