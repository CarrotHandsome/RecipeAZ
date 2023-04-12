using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class AddTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagId",
                table: "RecipeTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Tags");

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
        }
    }
}
