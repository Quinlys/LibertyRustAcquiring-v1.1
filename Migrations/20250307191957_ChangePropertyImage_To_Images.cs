using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibertyRustAcquiring.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyImage_To_Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Packs",
                newName: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Packs",
                newName: "Image");
        }
    }
}
