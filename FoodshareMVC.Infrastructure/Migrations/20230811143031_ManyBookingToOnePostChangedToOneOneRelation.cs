using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodshareMVC.Infrastructure.Migrations
{
    public partial class ManyBookingToOnePostChangedToOneOneRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_PostId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PostId",
                table: "Bookings",
                column: "PostId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_PostId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PostId",
                table: "Bookings",
                column: "PostId");
        }
    }
}
