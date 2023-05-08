using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class MakeBeforesAftersNonNullInIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 19, 45, 22, 802, DateTimeKind.Local).AddTicks(159));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b56ca097-d586-4f97-bb52-010a25418182", "AQAAAAIAAYagAAAAEJM+HTfNemH9ACzZc655p7vCR+IUts0z56kdP+45danvCuDSzDiJEfKrcGGM1iSXig==", "4fc8583c-34e3-4706-886a-fba40f24ec45" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 19, 33, 29, 922, DateTimeKind.Local).AddTicks(6212));

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2023, 5, 4, 19, 33, 29, 922, DateTimeKind.Local).AddTicks(5166));
        }
    }
}
