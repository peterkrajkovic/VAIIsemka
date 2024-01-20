using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApi.Migrations
{
    public partial class PostsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Post",
                newName: "Image3");

            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image1",
                table: "Post",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image2",
                table: "Post",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Image3",
                table: "Post",
                newName: "Image");
        }
    }
}
