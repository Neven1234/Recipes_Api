using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class userNameAddedAsAkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19a55dd7-f1f9-4e66-8b0b-0686e30962e6");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "recipes");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b0dd969-950c-4d3c-a966-077cb159840f", "1", "User", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b0dd969-950c-4d3c-a966-077cb159840f");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "recipes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "recipes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19a55dd7-f1f9-4e66-8b0b-0686e30962e6", "1", "User", "User" });
        }
    }
}
