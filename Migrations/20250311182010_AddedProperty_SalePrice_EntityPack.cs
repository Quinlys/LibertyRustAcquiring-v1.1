using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibertyRustAcquiring.Migrations
{
    /// <inheritdoc />
    public partial class AddedProperty_SalePrice_EntityPack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Packs",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Packs");
        }
    }
}
