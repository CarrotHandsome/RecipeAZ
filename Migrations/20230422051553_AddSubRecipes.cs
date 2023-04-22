using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class AddSubRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "RecipeSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubRecipeId",
                table: "RecipeSteps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "RecipeIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubRecipe",
                columns: table => new
                {
                    SubRecipeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRecipe", x => x.SubRecipeId);
                    table.ForeignKey(
                        name: "FK_SubRecipe_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03a2a352-58a9-4cc8-995f-46c5dce7e3ce", "AQAAAAIAAYagAAAAEAqsH3EFvkD8Weg7tIyE4c4/ZFMTfJ6JJFS+BSthtvRHkIR3CnY1pFClohr7M1g//g==", "c9fbc6bd-a954-4ad4-aee3-bb1613c23ce8" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 21, 22, 15, 53, 60, DateTimeKind.Local).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumn: "RecipeIngredientId",
                keyValue: "1",
                column: "Order",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumn: "RecipeIngredientId",
                keyValue: "2",
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RecipeSteps",
                keyColumn: "RecipeStepId",
                keyValue: "1",
                columns: new[] { "Order", "SubRecipeId" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "RecipeSteps",
                keyColumn: "RecipeStepId",
                keyValue: "2",
                columns: new[] { "Order", "SubRecipeId" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "RecipeSteps",
                keyColumn: "RecipeStepId",
                keyValue: "3",
                columns: new[] { "Order", "SubRecipeId" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 4, 21, 22, 15, 53, 59, DateTimeKind.Local).AddTicks(9062));

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_SubRecipeId",
                table: "RecipeSteps",
                column: "SubRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubRecipe_RecipeId",
                table: "SubRecipe",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSteps_SubRecipe_SubRecipeId",
                table: "RecipeSteps",
                column: "SubRecipeId",
                principalTable: "SubRecipe",
                principalColumn: "SubRecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSteps_SubRecipe_SubRecipeId",
                table: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "SubRecipe");

            migrationBuilder.DropIndex(
                name: "IX_RecipeSteps_SubRecipeId",
                table: "RecipeSteps");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "RecipeSteps");

            migrationBuilder.DropColumn(
                name: "SubRecipeId",
                table: "RecipeSteps");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "RecipeIngredients");

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
                column: "CreatedAt",
                value: new DateTime(2023, 4, 14, 0, 23, 30, 979, DateTimeKind.Local).AddTicks(9365));
        }
    }
}
