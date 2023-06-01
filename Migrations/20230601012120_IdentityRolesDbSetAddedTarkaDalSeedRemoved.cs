using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class IdentityRolesDbSetAddedTarkaDalSeedRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "RecipeIngredientId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "RecipeIngredientId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "RecipeSteps",
                keyColumn: "RecipeStepId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "RecipeSteps",
                keyColumn: "RecipeStepId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "RecipeSteps",
                keyColumn: "RecipeStepId",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "IngredientModifier",
                keyColumn: "IngredientModifierId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "IngredientModifier",
                keyColumn: "IngredientModifierId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77619ccc-8cbb-4593-a645-20489c80e8da", "AQAAAAIAAYagAAAAEBsFAV1G3eEwULKwFlmD1Ek4RK3txlRQQiOkQaBcNUUh6DxHIlfheLqw7jFO6za0Cw==", "518fa5c8-ec6a-49fb-952c-aff1b8833af1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44689390-4700-4eeb-a103-9342dc0847d9", "AQAAAAIAAYagAAAAEGCajBUIu4GtyEuUiYfuAgUvPtRpT4fgEJDodody9UxB1iHqvPDl4R+gaM+5fxHJxg==", "cb206ca4-7710-4d45-aa1d-74d247ad0502" });

            migrationBuilder.InsertData(
                table: "IngredientModifier",
                columns: new[] { "IngredientModifierId", "IngredientId", "IngredientId1", "IsBefore", "Name" },
                values: new object[,]
                {
                    { "1", null, null, false, "" },
                    { "2", null, null, true, "red" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name" },
                values: new object[,]
                {
                    { "1", "water" },
                    { "2", "red lentils" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "CreatedAt", "Description", "ImagePath", "Name", "Notes", "ParentRecipeId", "UserId" },
                values: new object[] { "1", new DateTime(2023, 5, 21, 14, 39, 26, 405, DateTimeKind.Local).AddTicks(9135), "Nulla turpis risus, mollis sed mi non, congue posuere enim. Fusce vehicula ligula nec nibh vehicula tempor. Maecenas accumsan quis mauris a imperdiet. Phasellus venenatis, dolor vitae venenatis aliquet, mi nisi consequat ligula, vel facilisis arcu eros id justo. Vestibulum id porta tellus, nec porttitor diam. Praesent hendrerit nulla sed eros finibus, eu sodales mauris semper. Praesent eros velit, sollicitudin a ex id, sagittis eleifend turpis. Pellentesque facilisis sem eu varius elementum. Sed ac justo dolor. Donec malesuada justo eu urna luctus, et cursus magna laoreet. Vivamus a accumsan risus. Nullam rutrum porta elementum. Curabitur lectus odio, euismod et dolor sit amet, efficitur elementum felis. Mauris quam sapien, commodo sed libero facilisis, cursus feugiat ligula.", "images/recipe_default.png", "Tarka Dal", "Donec quis ullamcorper erat, id tempor ante. Integer ut lorem viverra, laoreet orci nec, aliquam nisl. Fusce placerat nisl ac mauris condimentum, sed efficitur urna faucibus. Suspendisse laoreet laoreet malesuada. Quisque congue ut leo ac rhoncus. Aenean commodo dui ut urna ullamcorper tristique. Proin ac feugiat enim, vitae tempus magna. Nam suscipit luctus maximus. Sed laoreet dolor nibh, quis vestibulum leo facilisis a. Integer at mi convallis, porttitor leo non, eleifend enim. Mauris et dolor diam. Morbi metus ligula, pretium ac lectus in, tempus ullamcorper leo.", null, "02174cf0–9412–4cfe - afbf - 59f706d72cf6" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CreatedAt", "RecipeId", "Text", "UserId" },
                values: new object[] { "1", new DateTime(2023, 5, 21, 14, 39, 26, 406, DateTimeKind.Local).AddTicks(40), "1", "nice recipe", "02174cf0–9412–4cfe - afbf - 59f706d72cf6" });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "RecipeIngredientId", "AfterId", "Amount", "BeforeId", "IngredientId", "Name", "Notes", "Order", "RecipeId" },
                values: new object[,]
                {
                    { "1", "1", "4 cups", "2", "2", "", "", 1, "1" },
                    { "2", "1", "to cover", "1", "1", "", "", 2, "1" }
                });

            migrationBuilder.InsertData(
                table: "RecipeSteps",
                columns: new[] { "RecipeStepId", "Description", "Details", "Name", "Order", "RecipeId" },
                values: new object[,]
                {
                    { "1", "Rinse Lentils", "", "Step 1", 1, "1" },
                    { "2", "Add lentils and water to pot", "", "Step 2", 2, "1" },
                    { "3", "Boil till cooked", "", "Step 3", 3, "1" }
                });
        }
    }
}
