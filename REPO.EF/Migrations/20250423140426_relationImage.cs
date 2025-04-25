using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class relationImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //  migrationBuilder.DropForeignKey(
            //      name: "FK_Images_Regions_RegionId",
            //      table: "Images");
            //
            //  migrationBuilder.DropForeignKey(
            //      name: "FK_Images_Walks_WalkId",
            //      table: "Images");
            //
            //  migrationBuilder.DeleteData(
            //      table: "AspNetRoles",
            //      keyColumn: "Id",
            //      keyValue: "113fec3a-3d2b-4b27-894a-1e9fe1fadd4a");
            //
            //  migrationBuilder.DeleteData(
            //      table: "AspNetRoles",
            //      keyColumn: "Id",
            //      keyValue: "8c48cc86-ea40-459a-a686-d11a88b7f9b0");
            //
            //  migrationBuilder.DeleteData(
            //      table: "AspNetRoles",
            //      keyColumn: "Id",
            //      keyValue: "e167b10d-9e99-4feb-87b9-4bcc2f38fd8b");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "612b87f4-c9b8-4e00-b8fd-7026f67b1c63", "612b87f4-c9b8-4e00-b8fd-7026f67b1c63", "Reader", "READER" },
            //         { "67fc26a5-16fe-4fc7-a2e6-78c1c45d785c", "67fc26a5-16fe-4fc7-a2e6-78c1c45d785c", "Writer", "WRITER" },
            //         { "d9083a1b-19e1-4f28-82fd-c1479e52e788", "d9083a1b-19e1-4f28-82fd-c1479e52e788", "Admin", "ADMIN" }
            //     });


            // Add RegionId and WalkId columns to the Images table
            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "Images",
                nullable: true); // Nullable for SetNull delete behavior

            migrationBuilder.AddColumn<Guid>(
                name: "WalkId",
                table: "Images",
                nullable: true); // Nullable for SetNull delete behavior

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Regions_RegionId",
                table: "Images",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Walks_WalkId",
                table: "Images",
                column: "WalkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Regions_RegionId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Walks_WalkId",
                table: "Images");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "113fec3a-3d2b-4b27-894a-1e9fe1fadd4a", "113fec3a-3d2b-4b27-894a-1e9fe1fadd4a", "Admin", "ADMIN" },
                    { "8c48cc86-ea40-459a-a686-d11a88b7f9b0", "8c48cc86-ea40-459a-a686-d11a88b7f9b0", "Writer", "WRITER" },
                    { "e167b10d-9e99-4feb-87b9-4bcc2f38fd8b", "e167b10d-9e99-4feb-87b9-4bcc2f38fd8b", "Reader", "READER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Regions_RegionId",
                table: "Images",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Walks_WalkId",
                table: "Images",
                column: "WalkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
