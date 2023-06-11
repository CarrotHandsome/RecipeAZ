using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAZ.Migrations
{
    /// <inheritdoc />
    public partial class OptionalItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Optional",
                table: "RecipeSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Optional",
                table: "RecipeIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05def0ec-eb2c-4cb9-850d-5fd3b027e645", "AQAAAAIAAYagAAAAEEhOTHEKVNv/FxPmWn94j4LgzEU06gBKUJzfv6o5zPTPI4cIdKUT3RFqGwxlbRBFsQ==", "62111856-84c8-4ea8-891a-1d4a010ed089" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Optional",
                table: "RecipeSteps");

            migrationBuilder.DropColumn(
                name: "Optional",
                table: "RecipeIngredients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ecc1cf4-344b-4c83-84ae-8221977feb0c", "AQAAAAIAAYagAAAAEO6rq2qhuFPqIK9s61Modu74p05EhXJwxCsD0l1rTN8sCUxjskbJo37JUqcTwTtQzw==", "965af0a3-5212-4bb2-89d9-e475ff123e51" });
        }
    }
}
