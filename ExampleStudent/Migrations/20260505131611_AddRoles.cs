using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExampleStudent.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1646ba2a-3500-476e-aeb1-dcdab88f52f4", "3", "Editor", "Editor" },
                    { "4f244d50-9b55-485c-bb42-504a4a7f6b12", "2", "User", "User" },
                    { "96a1904c-f93f-40e7-9097-61900e48c5b5", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1646ba2a-3500-476e-aeb1-dcdab88f52f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f244d50-9b55-485c-bb42-504a4a7f6b12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96a1904c-f93f-40e7-9097-61900e48c5b5");
        }
    }
}
