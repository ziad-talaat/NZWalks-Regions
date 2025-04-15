using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class SeddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulty",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Easy" },
                    { 2, "Medium" },
                    { 3, "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("ce898fe2-16ad-403b-9d5f-dd0d98cad958"), "AKL", "Auckland", "image1" },
                    { new Guid("ce898fe2-26ad-403b-9d5f-dd0d98cad958"), "NSN", "Nelson", "image2" },
                    { new Guid("ce898fe2-36ad-403b-9d5f-dd0d98cad958"), "STL", "Southland", "image3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ce898fe2-16ad-403b-9d5f-dd0d98cad958"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ce898fe2-26ad-403b-9d5f-dd0d98cad958"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ce898fe2-36ad-403b-9d5f-dd0d98cad958"));
        }
    }
}
