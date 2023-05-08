using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class MakeModifiersNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Modifiers_AfterId",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<string>(
                name: "AfterId",
                table: "RecipeIngredients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Modifiers_AfterId",
                table: "RecipeIngredients",
                column: "AfterId",
                principalTable: "Modifiers",
                principalColumn: "IngredientModifierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Modifiers_AfterId",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<string>(
                name: "AfterId",
                table: "RecipeIngredients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Modifiers_AfterId",
                table: "RecipeIngredients",
                column: "AfterId",
                principalTable: "Modifiers",
                principalColumn: "IngredientModifierId");
        }
    }
}
