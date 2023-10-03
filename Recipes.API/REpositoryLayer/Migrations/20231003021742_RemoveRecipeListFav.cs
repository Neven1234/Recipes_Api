using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRecipeListFav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favorites_recipes_RecipeId",
                table: "favorites");

            migrationBuilder.DropIndex(
                name: "IX_favorites_RecipeId",
                table: "favorites");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5c6d116-2e37-4cde-930d-606399329f18");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7cad8ab0-8e59-4e1f-9f80-cbd3d5eb46f2", "1", "User", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cad8ab0-8e59-4e1f-9f80-cbd3d5eb46f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e5c6d116-2e37-4cde-930d-606399329f18", "1", "User", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_favorites_RecipeId",
                table: "favorites",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_favorites_recipes_RecipeId",
                table: "favorites",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
