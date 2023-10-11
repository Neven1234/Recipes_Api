using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class RecipeIdAndDatePublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbf86227-b432-4ac2-b38c-861372b15962");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "planner",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "planner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "580a903b-94a6-41c0-a7b6-24cdaf199e36", "1", "User", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "580a903b-94a6-41c0-a7b6-24cdaf199e36");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "planner");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "planner");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbf86227-b432-4ac2-b38c-861372b15962", "1", "User", "User" });
        }
    }
}
