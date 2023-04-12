using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87743dad-c858-42db-b029-63c2b476d2c2", "AQAAAAIAAYagAAAAEEiigxENen8KRekTP8P1NHtFXpLfktEkLrKazRVhim10EKMtjtVnWH4YvB4B0xM9PQ==", "38c02fd1-bcf4-4e9d-b753-a13b538a5f59" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 10, 23, 28, 45, 296, DateTimeKind.Local).AddTicks(592));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 10, 23, 28, 45, 295, DateTimeKind.Local).AddTicks(8814));
        }
    }
}
