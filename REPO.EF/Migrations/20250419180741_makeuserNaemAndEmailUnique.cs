using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class makeuserNaemAndEmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73f9722f-af96-431b-82c0-38f13bfec069");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee0f0a03-b07a-4be7-974b-9983f31a7a05");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e932632e-d330-458a-96a5-08ee7fa0a9d5", "e932632e-d330-458a-96a5-08ee7fa0a9d5", "Writer", "WRITER" },
                    { "fbd09092-25ab-49b6-bffb-4fa7e04b47d2", "fbd09092-25ab-49b6-bffb-4fa7e04b47d2", "Reader", "READER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

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
                    { "73f9722f-af96-431b-82c0-38f13bfec069", "73f9722f-af96-431b-82c0-38f13bfec069", "Writer", "WRITER" },
                    { "ee0f0a03-b07a-4be7-974b-9983f31a7a05", "ee0f0a03-b07a-4be7-974b-9983f31a7a05", "Reader", "READER" }
                });
        }
    }
}
