using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIngredientModifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_IngredientModifier_AfterId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_IngredientModifier_BeforeId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "IngredientModifier");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_AfterId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_BeforeId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "AfterId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "BeforeId",
                table: "RecipeIngredients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7be70b1-025c-43fa-8b26-98b0bf6057c4", "AQAAAAIAAYagAAAAELUounx1QAcKIZs4pyT7Ic8QmBoBK+X89z9VSJlKuscFsPyR3gQtlVVhDYS9BX6MbQ==", "83846a1b-089e-4693-9236-b1eaee5532ec" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AfterId",
                table: "RecipeIngredients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BeforeId",
                table: "RecipeIngredients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientModifier",
                columns: table => new
                {
                    IngredientModifierId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IngredientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IngredientId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsBefore = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientModifier", x => x.IngredientModifierId);
                    table.ForeignKey(
                        name: "FK_IngredientModifier_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId");
                    table.ForeignKey(
                        name: "FK_IngredientModifier_Ingredients_IngredientId1",
                        column: x => x.IngredientId1,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77619ccc-8cbb-4593-a645-20489c80e8da", "AQAAAAIAAYagAAAAEBsFAV1G3eEwULKwFlmD1Ek4RK3txlRQQiOkQaBcNUUh6DxHIlfheLqw7jFO6za0Cw==", "518fa5c8-ec6a-49fb-952c-aff1b8833af1" });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_AfterId",
                table: "RecipeIngredients",
                column: "AfterId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_BeforeId",
                table: "RecipeIngredients",
                column: "BeforeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientModifier_IngredientId",
                table: "IngredientModifier",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientModifier_IngredientId1",
                table: "IngredientModifier",
                column: "IngredientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_IngredientModifier_AfterId",
                table: "RecipeIngredients",
                column: "AfterId",
                principalTable: "IngredientModifier",
                principalColumn: "IngredientModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_IngredientModifier_BeforeId",
                table: "RecipeIngredients",
                column: "BeforeId",
                principalTable: "IngredientModifier",
                principalColumn: "IngredientModifierId");
        }
    }
}
