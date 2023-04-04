using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class AddIndicesForRecipeLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9df6193d-6b90-4599-a989-88981e4cb764", "AQAAAAIAAYagAAAAEHM/SNlYf1uYm+JcstFvBRuOITt5B5qEmvvi1Gujc/XE7kmoTvfUHZYdWuXf/PnzUQ==", "edde2359-c850-451f-8620-584d1f8564ac" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 3, 26, 17, 42, 56, 563, DateTimeKind.Local).AddTicks(1739));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 3, 26, 17, 42, 56, 562, DateTimeKind.Local).AddTicks(9473));

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLike_AppUserId",
                table: "RecipeLike",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeLike_AppUserId",
                table: "RecipeLike");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36f302e0-1b02-4f14-9947-b67c3e240d3c", "AQAAAAIAAYagAAAAELCHI766pO0mh9V4YgGdilfNT4mIEVH95Byxm5m9RauSUxMP7jdA5wFvTqYyJwuaxA==", "bf47b16f-c95b-46a8-831c-dffa402a5714" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 3, 26, 17, 29, 10, 663, DateTimeKind.Local).AddTicks(1085));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 3, 26, 17, 29, 10, 662, DateTimeKind.Local).AddTicks(9355));
        }
    }
}
