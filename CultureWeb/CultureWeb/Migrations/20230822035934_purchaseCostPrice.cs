using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CultureWeb.Migrations
{
    /// <inheritdoc />
    public partial class purchaseCostPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostPrice",
                table: "PurchaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "PurchaseDetails");
        }
    }
}
