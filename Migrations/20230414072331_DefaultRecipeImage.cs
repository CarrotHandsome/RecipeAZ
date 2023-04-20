using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class DefaultRecipeImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05f03e8f-29ed-4523-9d6d-22c310fad19d", "AQAAAAIAAYagAAAAELZLzt7n2/vB8wb2QX//AJ/OK+XewmHpmNrFiFSzzVI58dvTjwCjH+evlW5f4fe0bA==", "b18beda3-2403-4a07-a041-74df3073f6a1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 14, 0, 23, 30, 980, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2023, 4, 14, 0, 23, 30, 979, DateTimeKind.Local).AddTicks(9365), "images/recipe_default.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d109ccb-35b7-4fb2-9f85-aa739a22f00e", "AQAAAAIAAYagAAAAEA3JvbtTYo9F4pY8L1TrjvHHcO4PeRPR5/A7xznyl+MqwvyPbCdxLr9ghYebrV8e4A==", "0e8eed00-07f4-44fe-96d1-8b034363eff4" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 11, 22, 57, 33, 275, DateTimeKind.Local).AddTicks(608));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                columns: new[] { "CreatedAt", "ImagePath" },
                values: new object[] { new DateTime(2023, 4, 11, 22, 57, 33, 274, DateTimeKind.Local).AddTicks(8816), null });
        }
    }
}
