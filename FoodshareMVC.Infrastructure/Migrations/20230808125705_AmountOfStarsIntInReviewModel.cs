using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodshareMVC.Infrastructure.Migrations
{
    public partial class AmountOfStarsIntInReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AmountOfStars",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "AmountOfStars",
                table: "Reviews",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
