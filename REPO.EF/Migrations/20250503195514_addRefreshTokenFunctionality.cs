using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class addRefreshTokenFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0376d158-3763-4867-9bd8-a5c4527083a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f80d06d-19c9-4cdd-8485-0730827cab35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6300d7b0-28cd-45c3-968b-fc714db887af");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationRefreshToken",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefrehToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

          // migrationBuilder.InsertData(
          //     table: "AspNetRoles",
          //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
          //     values: new object[,]
          //     {
          //         { "2d23fcfe-99e5-45b3-ad8f-4e06734d6b16", "2d23fcfe-99e5-45b3-ad8f-4e06734d6b16", "Reader", "READER" },
          //         { "6e2c22e1-5d35-407f-803f-2c8722f76704", "6e2c22e1-5d35-407f-803f-2c8722f76704", "Writer", "WRITER" },
          //         { "dc910130-c490-435a-8fbc-20e27d973946", "dc910130-c490-435a-8fbc-20e27d973946", "Admin", "ADMIN" }
          //     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d23fcfe-99e5-45b3-ad8f-4e06734d6b16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e2c22e1-5d35-407f-803f-2c8722f76704");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc910130-c490-435a-8fbc-20e27d973946");

            migrationBuilder.DropColumn(
                name: "ExpirationRefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefrehToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0376d158-3763-4867-9bd8-a5c4527083a9", "0376d158-3763-4867-9bd8-a5c4527083a9", "Admin", "ADMIN" },
                    { "3f80d06d-19c9-4cdd-8485-0730827cab35", "3f80d06d-19c9-4cdd-8485-0730827cab35", "Writer", "WRITER" },
                    { "6300d7b0-28cd-45c3-968b-fc714db887af", "6300d7b0-28cd-45c3-968b-fc714db887af", "Reader", "READER" }
                });
        }
    }
}
