using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserFiledsForRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1256f5e8-e88b-4773-a5be-f67dc4aa0bd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6faa1203-dc18-456c-9137-ee289c35e504");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9140564-53fd-444e-ac77-863151307259", "8e870063-a3aa-467e-b3ad-274749688c2a", "Administrator", "ADMINISTRATOR" },
                    { "c662dd71-3c32-4698-9d0b-978f4b030ac6", "2ac31188-e254-4248-bde3-fe48a26bc859", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9140564-53fd-444e-ac77-863151307259");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c662dd71-3c32-4698-9d0b-978f4b030ac6");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1256f5e8-e88b-4773-a5be-f67dc4aa0bd9", "f34e247e-ec52-46e4-8b98-2edf92c6abbc", "Administrator", "ADMINISTRATOR" },
                    { "6faa1203-dc18-456c-9137-ee289c35e504", "e600ebba-3f31-4fe6-876b-a5ab929a0f62", "Manager", "MANAGER" }
                });
        }
    }
}
