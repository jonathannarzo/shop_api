using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shop.Migrations
{
    /// <inheritdoc />
    public partial class roleinheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93847503-c85a-4fc1-addc-12efb734b0c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd2f5088-0bb3-4206-bab3-215ccd807c25");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ecdba99-a698-4d42-a964-9e919e350aa2", null, "IdentityRole", "User", "USER" },
                    { "67f0ba17-e12c-44f1-92e9-15fe927dba65", null, "IdentityRole", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ecdba99-a698-4d42-a964-9e919e350aa2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67f0ba17-e12c-44f1-92e9-15fe927dba65");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93847503-c85a-4fc1-addc-12efb734b0c5", null, "User", "USER" },
                    { "fd2f5088-0bb3-4206-bab3-215ccd807c25", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
