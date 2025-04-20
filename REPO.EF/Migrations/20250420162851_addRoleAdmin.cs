using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class addRoleAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e932632e-d330-458a-96a5-08ee7fa0a9d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd09092-25ab-49b6-bffb-4fa7e04b47d2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36f35ada-4a2e-4b28-8f4a-15733cb5cf56", "36f35ada-4a2e-4b28-8f4a-15733cb5cf56", "Reader", "READER" },
                    { "853228c1-8b22-44a7-b98e-497d3563cbb7", "853228c1-8b22-44a7-b98e-497d3563cbb7", "Writer", "WRITER" },
                    { "d4b4cd98-70ea-47b0-a28c-3a27b9e8bb30", "d4b4cd98-70ea-47b0-a28c-3a27b9e8bb30", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36f35ada-4a2e-4b28-8f4a-15733cb5cf56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "853228c1-8b22-44a7-b98e-497d3563cbb7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4b4cd98-70ea-47b0-a28c-3a27b9e8bb30");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e932632e-d330-458a-96a5-08ee7fa0a9d5", "e932632e-d330-458a-96a5-08ee7fa0a9d5", "Writer", "WRITER" },
                    { "fbd09092-25ab-49b6-bffb-4fa7e04b47d2", "fbd09092-25ab-49b6-bffb-4fa7e04b47d2", "Reader", "READER" }
                });
        }
    }
}
