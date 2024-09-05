using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeeRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed02801 - 13ef - 4343 - 8e1c - a846908efdf4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74186481 - d066 - 406d - bcc1 - d3154ef10a20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ed02801-13ef-4343-8e1c-a846908efdf4", null, "Administrador", "ADMINISTRADOR" },
                    { "74186481-d066-406d-bcc1-d3154ef10a20", null, "Cliente", "CLIENTE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed02801-13ef-4343-8e1c-a846908efdf4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74186481-d066-406d-bcc1-d3154ef10a20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ed02801 - 13ef - 4343 - 8e1c - a846908efdf4", null, "Administrator", "Admin" },
                    { "74186481 - d066 - 406d - bcc1 - d3154ef10a20", null, "Cliente", "Client" }
                });
        }
    }
}
