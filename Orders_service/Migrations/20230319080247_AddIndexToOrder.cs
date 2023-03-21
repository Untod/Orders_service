using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders_service.Migrations
{
    public partial class AddIndexToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_ProviderId",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProviderId_Number",
                table: "Order",
                columns: new[] { "ProviderId", "Number" },
                unique: true,
                filter: "[ProviderId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_ProviderId_Number",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProviderId",
                table: "Order",
                column: "ProviderId");
        }
    }
}
