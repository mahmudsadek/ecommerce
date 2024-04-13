using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_cartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipment_ShipmentId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShipmentId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_cartId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "cartId",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShipmentId",
                table: "Order",
                column: "ShipmentId",
                unique: true,
                filter: "[ShipmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CarttId",
                table: "CartItem",
                column: "CarttId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CarttId",
                table: "CartItem",
                column: "CarttId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipment_ShipmentId",
                table: "Order",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CarttId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipment_ShipmentId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShipmentId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_CarttId",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cartId",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShipmentId",
                table: "Order",
                column: "ShipmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_cartId",
                table: "CartItem",
                column: "cartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_cartId",
                table: "CartItem",
                column: "cartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipment_ShipmentId",
                table: "Order",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
