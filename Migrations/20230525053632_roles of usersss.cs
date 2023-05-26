using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shop.Migrations
{
    /// <inheritdoc />
    public partial class rolesofusersss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ecdba99-a698-4d42-a964-9e919e350aa2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67f0ba17-e12c-44f1-92e9-15fe927dba65");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "180aceca-bdfd-43b8-bb0e-63616afaa45b", null, "IdentityRole", "Administrator", "ADMINISTRATOR" },
                    { "a28b596d-4180-4d44-90c6-20731c19a463", null, "IdentityRole", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "180aceca-bdfd-43b8-bb0e-63616afaa45b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a28b596d-4180-4d44-90c6-20731c19a463");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ecdba99-a698-4d42-a964-9e919e350aa2", null, "IdentityRole", "User", "USER" },
                    { "67f0ba17-e12c-44f1-92e9-15fe927dba65", null, "IdentityRole", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
