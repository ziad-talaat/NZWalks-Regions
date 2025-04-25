using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace REPO.EF.Migrations
{
    /// <inheritdoc />
    public partial class addrelationBetweenTheImageAndRegionAndWalks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "768732d7-2eea-4dfb-bebd-497c84600e06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b38f09fc-cd6e-4f6c-94db-4c3959997f37");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8c1bbba-e5f4-4643-904b-74160222346b");

          // migrationBuilder.InsertData(
          //     table: "AspNetRoles",
          //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
          //     values: new object[,]
          //     {
          //         { "67e39f26-1357-4f7f-a724-5f4ac41795d1", "67e39f26-1357-4f7f-a724-5f4ac41795d1", "Writer", "WRITER" },
          //         { "91a56541-2556-46d2-9b37-e7c06f8c5bcc", "91a56541-2556-46d2-9b37-e7c06f8c5bcc", "Reader", "READER" },
          //         { "ffd77de7-f507-4206-80d6-065f9a297464", "ffd77de7-f507-4206-80d6-065f9a297464", "Admin", "ADMIN" }
          //     });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e39f26-1357-4f7f-a724-5f4ac41795d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91a56541-2556-46d2-9b37-e7c06f8c5bcc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffd77de7-f507-4206-80d6-065f9a297464");

          //  migrationBuilder.InsertData(
          //      table: "AspNetRoles",
          //      columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
          //      values: new object[,]
          //      {
          //          { "768732d7-2eea-4dfb-bebd-497c84600e06", "768732d7-2eea-4dfb-bebd-497c84600e06", "Admin", "ADMIN" },
          //          { "b38f09fc-cd6e-4f6c-94db-4c3959997f37", "b38f09fc-cd6e-4f6c-94db-4c3959997f37", "Reader", "READER" },
          //          { "f8c1bbba-e5f4-4643-904b-74160222346b", "f8c1bbba-e5f4-4643-904b-74160222346b", "Writer", "WRITER" }
          //      });
        }
    }
}
