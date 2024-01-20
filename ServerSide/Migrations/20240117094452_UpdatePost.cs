using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApi.Migrations
{
    public partial class UpdatePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Post");

            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
