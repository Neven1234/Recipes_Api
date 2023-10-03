using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class ListOfRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5321be7-5ed1-4d7e-9a67-7bfaaf8f3968");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "d5321be7-5ed1-4d7e-9a67-7bfaaf8f3968", "1", "User", "User" });
        }
    }
}
