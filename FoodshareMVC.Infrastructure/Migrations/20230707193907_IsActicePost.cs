using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodshareMVC.Infrastructure.Migrations
{
    public partial class IsActicePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Posts");
        }
    }
}
