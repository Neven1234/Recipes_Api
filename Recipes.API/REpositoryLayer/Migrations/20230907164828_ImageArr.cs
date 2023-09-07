using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class ImageArr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "recipes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "recipes",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "recipes");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "recipes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
