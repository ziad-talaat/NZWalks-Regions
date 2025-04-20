using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class addingImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a5df88b-311e-4a78-b3fd-a2d1ac43b768", "3a5df88b-311e-4a78-b3fd-a2d1ac43b768", "Admin", "ADMIN" },
                    { "4e5b4ebf-9288-42ae-af6f-9e7590da898b", "4e5b4ebf-9288-42ae-af6f-9e7590da898b", "Reader", "READER" },
                    { "e71411ad-6f8e-4583-b8db-0583d9ab74f8", "e71411ad-6f8e-4583-b8db-0583d9ab74f8", "Writer", "WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a5df88b-311e-4a78-b3fd-a2d1ac43b768");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e5b4ebf-9288-42ae-af6f-9e7590da898b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e71411ad-6f8e-4583-b8db-0583d9ab74f8");

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
    }
}
