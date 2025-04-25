using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class addIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "612b87f4-c9b8-4e00-b8fd-7026f67b1c63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67fc26a5-16fe-4fc7-a2e6-78c1c45d785c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9083a1b-19e1-4f28-82fd-c1479e52e788");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Walks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Regions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "WalkId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

          // migrationBuilder.InsertData(
          //     table: "AspNetRoles",
          //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
          //     values: new object[,]
          //     {
          //         { "0376d158-3763-4867-9bd8-a5c4527083a9", "0376d158-3763-4867-9bd8-a5c4527083a9", "Admin", "ADMIN" },
          //         { "3f80d06d-19c9-4cdd-8485-0730827cab35", "3f80d06d-19c9-4cdd-8485-0730827cab35", "Writer", "WRITER" },
          //         { "6300d7b0-28cd-45c3-968b-fc714db887af", "6300d7b0-28cd-45c3-968b-fc714db887af", "Reader", "READER" }
          //     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Walks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Regions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "WalkId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "612b87f4-c9b8-4e00-b8fd-7026f67b1c63", "612b87f4-c9b8-4e00-b8fd-7026f67b1c63", "Reader", "READER" },
                    { "67fc26a5-16fe-4fc7-a2e6-78c1c45d785c", "67fc26a5-16fe-4fc7-a2e6-78c1c45d785c", "Writer", "WRITER" },
                    { "d9083a1b-19e1-4f28-82fd-c1479e52e788", "d9083a1b-19e1-4f28-82fd-c1479e52e788", "Admin", "ADMIN" }
                });
        }
    }
}
