using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class LikesToHashSetsAndHaveDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RecipeLike",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RecipeLike");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c081978-e976-420b-8c70-af7f01bf6837", "AQAAAAIAAYagAAAAEGx97AWyXkehWUxSvALIRFWHH2LjY1SEfIS5p5UjXu8sa505XrnnmgN00V28HLLAGQ==", "eeebd1c5-058b-4f13-bcca-a9acf4f6eab3" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 3, 22, 2, 21, 9, 745, DateTimeKind.Local).AddTicks(3372));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 3, 22, 2, 21, 9, 745, DateTimeKind.Local).AddTicks(1391));
        }
    }
}
