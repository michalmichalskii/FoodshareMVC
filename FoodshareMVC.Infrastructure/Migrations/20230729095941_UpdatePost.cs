using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodshareMVC.Infrastructure.Migrations
{
    public partial class UpdatePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PossibilityPickUpMethod",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PossibilityPickUpMethod",
                table: "Posts");
        }
    }
}
