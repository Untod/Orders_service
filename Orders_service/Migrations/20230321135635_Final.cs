using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders_service.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Provider_ProviderId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ProviderId_Number",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProviderId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProviderId_Number",
                table: "Order",
                columns: new[] { "ProviderId", "Number" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Provider_ProviderId",
                table: "Order",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Provider_ProviderId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ProviderId_Number",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ProviderId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProviderId_Number",
                table: "Order",
                columns: new[] { "ProviderId", "Number" },
                unique: true,
                filter: "[ProviderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Provider_ProviderId",
                table: "Order",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id");
        }
    }
}
