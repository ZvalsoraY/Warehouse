using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class StockProductToWarehouseProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockProduct_Product_ProductId",
                table: "StockProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StockProduct_WarehouseInformation_WarehouseId",
                table: "StockProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockProduct",
                table: "StockProduct");

            migrationBuilder.RenameTable(
                name: "StockProduct",
                newName: "WarehouseProduct");

            migrationBuilder.RenameIndex(
                name: "IX_StockProduct_WarehouseId",
                table: "WarehouseProduct",
                newName: "IX_WarehouseProduct_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_StockProduct_ProductId",
                table: "WarehouseProduct",
                newName: "IX_WarehouseProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProduct",
                table: "WarehouseProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_Product_ProductId",
                table: "WarehouseProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_WarehouseInformation_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId",
                principalTable: "WarehouseInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_Product_ProductId",
                table: "WarehouseProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_WarehouseInformation_WarehouseId",
                table: "WarehouseProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProduct",
                table: "WarehouseProduct");

            migrationBuilder.RenameTable(
                name: "WarehouseProduct",
                newName: "StockProduct");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseProduct_WarehouseId",
                table: "StockProduct",
                newName: "IX_StockProduct_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseProduct_ProductId",
                table: "StockProduct",
                newName: "IX_StockProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockProduct",
                table: "StockProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockProduct_Product_ProductId",
                table: "StockProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockProduct_WarehouseInformation_WarehouseId",
                table: "StockProduct",
                column: "WarehouseId",
                principalTable: "WarehouseInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
