using Microsoft.EntityFrameworkCore.Migrations;

namespace ItTalks.Data.Migrations
{
    public partial class PostsTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForumText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ForumTopic",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Messega",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Messega",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "ForumText",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForumTopic",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
