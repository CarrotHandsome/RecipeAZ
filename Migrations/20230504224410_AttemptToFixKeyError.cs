using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class AttemptToFixKeyError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98a7afba-bc4f-43af-81cf-e9eb2ac3f9a5", "AQAAAAIAAYagAAAAEGOil990IhHoWRxfXrMMwGZtE4pSlLgfeRj6UEB2XdoXpBfctpBVZ4eogD8Cw9r9FA==", "352626ad-1436-47c5-97b8-13bd9ec2f6c7" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 15, 44, 10, 321, DateTimeKind.Local).AddTicks(189));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 15, 44, 10, 320, DateTimeKind.Local).AddTicks(9092));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ef560e3-2e0b-4074-91b5-95d5c9d36fbb", "AQAAAAIAAYagAAAAEBzuiq4sTqOXxj9/470LrzDmS/XgJOGFy4a3BE4Y8DW8jkCZisSrIFFAdS1CHc9B+w==", "d487ec1d-d068-4c0a-8a4b-25e487e341fc" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 12, 42, 54, 415, DateTimeKind.Local).AddTicks(5906));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 12, 42, 54, 415, DateTimeKind.Local).AddTicks(4876));
        }
    }
}
