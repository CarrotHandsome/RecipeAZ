using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class AddTagDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Recipes_RecipeId",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Tags_TagId",
                table: "RecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag");

            migrationBuilder.RenameTable(
                name: "RecipeTag",
                newName: "RecipeTags");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTag_TagId",
                table: "RecipeTags",
                newName: "IX_RecipeTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags",
                columns: new[] { "RecipeId", "TagId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                table: "RecipeTags",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                table: "RecipeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                table: "RecipeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags");

            migrationBuilder.RenameTable(
                name: "RecipeTags",
                newName: "RecipeTag");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTags_TagId",
                table: "RecipeTag",
                newName: "IX_RecipeTag_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag",
                columns: new[] { "RecipeId", "TagId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae9f8a6c-8190-4340-9eaa-8ed365b27c92", "AQAAAAIAAYagAAAAELFCvI4nOAc7FekEQm0vOb/FYOySyh6qaLm1WuHWbCbKIeUQocogqjXlpN72wN4L4w==", "24640635-49c8-4d8e-b716-77b2caa5c142" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 10, 17, 4, 13, 215, DateTimeKind.Local).AddTicks(6314));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 10, 17, 4, 13, 215, DateTimeKind.Local).AddTicks(4657));

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Recipes_RecipeId",
                table: "RecipeTag",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Tags_TagId",
                table: "RecipeTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
