using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CultureWeb.Migrations
{
    /// <inheritdoc />
    public partial class tbl_productprice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPrices_ProductPriceId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductPriceId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPriceId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductPriceId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductPriceId",
                table: "Products",
                column: "ProductPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPrices_ProductPriceId",
                table: "Products",
                column: "ProductPriceId",
                principalTable: "ProductPrices",
                principalColumn: "Id");
        }
    }
}
