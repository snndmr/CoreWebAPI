using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class EnableLegacyTimestampBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9140564-53fd-444e-ac77-863151307259");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c662dd71-3c32-4698-9d0b-978f4b030ac6");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "80fb0577-26cf-45a1-871d-575bf616e888", "66d48c49-6078-44f6-88b4-41d94274decd", "Manager", "MANAGER" },
                    { "bc3eda9d-433a-4b58-8048-7237b8c62700", "174128ba-7a7c-4abd-a3e2-b6518d6cb1d5", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80fb0577-26cf-45a1-871d-575bf616e888");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc3eda9d-433a-4b58-8048-7237b8c62700");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9140564-53fd-444e-ac77-863151307259", "8e870063-a3aa-467e-b3ad-274749688c2a", "Administrator", "ADMINISTRATOR" },
                    { "c662dd71-3c32-4698-9d0b-978f4b030ac6", "2ac31188-e254-4248-bde3-fe48a26bc859", "Manager", "MANAGER" }
                });
        }
    }
}
