using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class categoryFiledAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7112b5bd-52da-4c19-abba-230f0bdd1d00");

            migrationBuilder.RenameColumn(
                name: "Categor",
                table: "recipes",
                newName: "Category");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "093ac8b9-dbac-4014-972b-4c0a3fb7ba9b", "1", "User", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "093ac8b9-dbac-4014-972b-4c0a3fb7ba9b");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "recipes",
                newName: "Categor");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7112b5bd-52da-4c19-abba-230f0bdd1d00", "1", "User", "User" });
        }
    }
}
