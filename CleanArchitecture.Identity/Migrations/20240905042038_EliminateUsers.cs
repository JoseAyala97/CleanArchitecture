using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Identity.Migrations
{
    /// <inheritdoc />
    public partial class EliminateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5ed02801 - 13ef - 4343 - 8e1c - a846908efdf4", "7602a591-c8ef-4687-80b3-81c00c1b8530" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "74186481 - d066 - 406d - bcc1 - d3154ef10a20", "8e6b6dcf-6e01-4ed4-9440-fe778fb72cc9" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7602a591-c8ef-4687-80b3-81c00c1b8530");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e6b6dcf-6e01-4ed4-9440-fe778fb72cc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74186481 - d066 - 406d - bcc1 - d3154ef10a20",
                column: "Name",
                value: "Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74186481 - d066 - 406d - bcc1 - d3154ef10a20",
                column: "Name",
                value: "Client");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7602a591-c8ef-4687-80b3-81c00c1b8530", 0, "5472c910-10f2-43bb-81cd-e63e24167e0f", "admin@localhost.com", true, "Ayala", false, null, "Jose", "admin@localhost.com", "Jose", "AQAAAAIAAYagAAAAEE029UzPdp0auK5NCEg+/IUqzjO+ha2yXgrGvkjjKoTyKBskYV+8GkQgjOkMbBp8eA==", null, false, "84992b69-06be-446e-b231-dbd24505af01", false, "JoseAyala" },
                    { "8e6b6dcf-6e01-4ed4-9440-fe778fb72cc9", 0, "a2669ac6-25ef-46f3-8a6f-7529da91066d", "luis@localhost2.com", true, "Pinto", false, null, "Luis", "luis@localhost2.com", "Luis", "AQAAAAIAAYagAAAAEPyWJvmwRsNfvKas+WQFcnWM+LbjvU5m2PXwkStZ2RUoVEJl2zNVlS8v1VC+Q74r1g==", null, false, "8f9ef4d7-bc61-46ad-9c52-0329b56d709a", false, "LuisPinto" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "5ed02801 - 13ef - 4343 - 8e1c - a846908efdf4", "7602a591-c8ef-4687-80b3-81c00c1b8530" },
                    { "74186481 - d066 - 406d - bcc1 - d3154ef10a20", "8e6b6dcf-6e01-4ed4-9440-fe778fb72cc9" }
                });
        }
    }
}
