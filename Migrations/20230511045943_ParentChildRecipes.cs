﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class ParentChildRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentRecipeId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "283db4b3-190f-4d8c-8bff-804c7deda0b3", "AQAAAAIAAYagAAAAEB8+yHUB2OxOjvgP5E6EnAXB6EX8mpn11wyGnsU5DttBzpWAJR+F8aY31K9YJl8ScA==", "87355706-8f11-46e1-81e8-a0ab2896c98e" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 10, 21, 59, 43, 2, DateTimeKind.Local).AddTicks(3854));

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: "2",
                column: "Name",
                value: "red lentils");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                columns: new[] { "CreatedAt", "ParentRecipeId" },
                values: new object[] { new DateTime(2023, 5, 10, 21, 59, 43, 2, DateTimeKind.Local).AddTicks(2764), null });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ParentRecipeId",
                table: "Recipes",
                column: "ParentRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Recipes_ParentRecipeId",
                table: "Recipes",
                column: "ParentRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Recipes_ParentRecipeId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ParentRecipeId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ParentRecipeId",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46389808-5369-48bc-bdd3-d012e763bb9f", "AQAAAAIAAYagAAAAEEs5599zY7ktXyVFqNgceVwstzhHeOH6mkI+fT6bac7ZYS2bRewp1acOXKxcPsIkOQ==", "3e58afd2-ff34-421d-a86c-f2ebdd1b98e1" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 19, 45, 22, 802, DateTimeKind.Local).AddTicks(1298));

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: "2",
                column: "Name",
                value: "lentils");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 19, 45, 22, 802, DateTimeKind.Local).AddTicks(159));
        }
    }
}
