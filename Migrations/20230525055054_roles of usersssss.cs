using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shop.Migrations
{
    /// <inheritdoc />
    public partial class rolesofusersssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "180aceca-bdfd-43b8-bb0e-63616afaa45b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a28b596d-4180-4d44-90c6-20731c19a463");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85a58610-e8e8-4c12-b784-a1c649e50d80", null, "IdentityRole", "Administrator", "ADMINISTRATOR" },
                    { "91454528-7d7a-468e-82b7-6c1ea4ec5c4e", null, "IdentityRole", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a58610-e8e8-4c12-b784-a1c649e50d80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91454528-7d7a-468e-82b7-6c1ea4ec5c4e");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "180aceca-bdfd-43b8-bb0e-63616afaa45b", null, "IdentityRole", "Administrator", "ADMINISTRATOR" },
                    { "a28b596d-4180-4d44-90c6-20731c19a463", null, "IdentityRole", "User", "USER" }
                });
        }
    }
}
