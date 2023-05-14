using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1256f5e8-e88b-4773-a5be-f67dc4aa0bd9", "f34e247e-ec52-46e4-8b98-2edf92c6abbc", "Administrator", "ADMINISTRATOR" },
                    { "6faa1203-dc18-456c-9137-ee289c35e504", "e600ebba-3f31-4fe6-876b-a5ab929a0f62", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1256f5e8-e88b-4773-a5be-f67dc4aa0bd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6faa1203-dc18-456c-9137-ee289c35e504");
        }
    }
}
