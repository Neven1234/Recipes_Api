using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class reviewsTble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RateAndReview_recipes_RecipeId",
                table: "RateAndReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateAndReview",
                table: "RateAndReview");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1928ed36-ed3b-446b-ac36-3732f852f771");

            migrationBuilder.RenameTable(
                name: "RateAndReview",
                newName: "reviews");

            migrationBuilder.RenameIndex(
                name: "IX_RateAndReview_RecipeId",
                table: "reviews",
                newName: "IX_reviews_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31a242a2-8b3a-4651-86f8-c8e804ec9a6a", "1", "User", "User" });

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_recipes_RecipeId",
                table: "reviews",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_recipes_RecipeId",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31a242a2-8b3a-4651-86f8-c8e804ec9a6a");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "RateAndReview");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_RecipeId",
                table: "RateAndReview",
                newName: "IX_RateAndReview_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateAndReview",
                table: "RateAndReview",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1928ed36-ed3b-446b-ac36-3732f852f771", "1", "User", "User" });

            migrationBuilder.AddForeignKey(
                name: "FK_RateAndReview_recipes_RecipeId",
                table: "RateAndReview",
                column: "RecipeId",
                principalTable: "recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
